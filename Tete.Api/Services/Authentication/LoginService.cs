using System;
using System.Linq;
using Tete.Api.Contexts;
using Tete.Api.Helpers;
using Tete.Api.Services.Users;
using Tete.Models.Authentication;

namespace Tete.Api.Services.Authentication
{
  public class LoginService
  {
    private MainContext mainContext;
    private ProfileService profileService;

    public LoginService(MainContext mainContext)
    {
      this.mainContext = mainContext;
      this.profileService = new ProfileService(mainContext);
    }

    public SessionVM Login(LoginAttempt login)
    {
      return GetNewToken(login);
    }

    public void Register(RegistrationAttempt registration)
    {
      if (this.mainContext.Users.Where(u => u.UserName == registration.UserName || u.Email == registration.Email).FirstOrDefault() == null)
      {
        byte[] salt = Crypto.NewSalt();
        string hash = Crypto.Hash(registration.Password, salt);
        var newUser = new User()
        {
          UserName = registration.UserName,
          Email = registration.Email,
          DisplayName = registration.DisplayName,
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
      }
      else
      {
        throw new Exception("User already exists!");
      }
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
        userVM = this.profileService.GetUser(user);
      }

      return userVM;
    }

    private SessionVM GetNewToken(LoginAttempt login)
    {
      // Select UserId from login where passwordhash = login.Password
      // Select true from user where userId = UserId and email = login.Email
      SessionVM sessionVM = null;
      Session session = null;
      var user = this.mainContext.Users.Where(u => u.UserName == login.UserName).FirstOrDefault();

      if (user != null)
      {
        string hash = Crypto.Hash(login.Password, user.Salt);

        var dbLogin = this.mainContext.Logins.Where(l => l.PasswordHash == hash && l.UserId == user.Id).FirstOrDefault();
        if (dbLogin != null)
        {
          string token = Crypto.Hash(Guid.NewGuid().ToString() + user.Id, user.Salt);
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