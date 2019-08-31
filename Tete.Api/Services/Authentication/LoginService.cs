using System;
using System.Collections.Generic;
using System.Linq;
using Tete.Api.Contexts;
using Tete.Models.Authentication;
using Tete.Api.Helpers;

namespace Tete.Api.Services.Authentication
{
  public class LoginService
  {
    private MainContext mainContext;

    public LoginService(MainContext mainContext)
    {
      this.mainContext = mainContext;
    }

    public SessionVM Login(LoginAttempt login)
    {
      return GetNewToken(login);
    }

    public SessionVM Register(LoginAttempt login)
    {
      byte[] salt = Crypto.NewSalt();
      string hash = Crypto.Hash(login.Password, salt);
      var newUser = new User()
      {
        Email = login.Email,
        Salt = salt
      };
      var newLogin = new Login()
      {
        UserId = newUser.Id,
        PasswordHash = hash
      };

      this.mainContext.Users.Add(newUser);
      this.mainContext.Logins.Add(newLogin);
      this.mainContext.SaveChanges();

      return GetNewToken(login);
    }

    public User GetUserFromToken(string token)
    {
      var session = this.mainContext.Sessions.Where(s => s.Token == token).FirstOrDefault();

      User user = null;
      if (session != null)
      {
        user = this.mainContext.Users.Where(u => u.Id == session.UserId).FirstOrDefault();
      }

      return user;
    }

    public UserVM GetUserVMFromToken(string token)
    {
      var user = GetUserFromToken(token);
      UserVM userVM = null;

      if (user != null)
      {
        userVM = new UserVM(user);
      }

      return userVM;
    }

    private SessionVM GetNewToken(LoginAttempt login)
    {
      // Select UserId from login where passwordhash = login.Password
      // Select true from user where userId = UserId and email = login.Email
      SessionVM sessionVM = null;
      Session session = null;
      var user = this.mainContext.Users.Where(u => u.Email == login.Email).FirstOrDefault();

      if (user != null)
      {
        string hash = Crypto.Hash(login.Password, user.Salt);

        var dbLogin = this.mainContext.Logins.Where(l => l.PasswordHash == hash && l.UserId == user.Id).FirstOrDefault();
        if (dbLogin != null)
        {
          string token = Crypto.Hash(Guid.NewGuid().ToString(), user.Salt);
          session = new Session()
          {
            UserId = user.Id,
            Token = token
          };
          this.mainContext.Sessions.Add(session);
          this.mainContext.SaveChanges();
        }
      }

      if (session != null)
      {
        sessionVM = new SessionVM(session);
      }

      return sessionVM;
    }
  }
}