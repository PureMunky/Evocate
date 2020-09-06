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

    //TODO: add logging for the login service.

    public LoginService(MainContext mainContext)
    {
      this.mainContext = mainContext;
    }

    /// <summary>
    /// Takes a login attempt and returns a valid sessionVM.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public SessionVM Login(LoginAttempt login)
    {
      return GetNewToken(login);
    }

    public void Logout(string Token)
    {
      var session = this.mainContext.Sessions.Where(s => s.Token == Token).FirstOrDefault();
      if (session != null)
      {
        this.mainContext.Sessions.Remove(session);
        this.mainContext.SaveChanges();
      }
    }

    /// <summary>
    /// Attempts to register a new user with the provided
    /// registration attempt.
    /// </summary>
    /// <param name="registration"></param>
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

    /// <summary>
    /// Parses a passed token and returns the user associated
    /// with that token.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public User GetUserFromToken(string token)
    {
      var session = this.mainContext.Sessions.Where(s => s.Token == token).FirstOrDefault();

      User user = null;
      if (session != null)
      {
        user = this.mainContext.Users.Where(u => u.Id == session.UserId).FirstOrDefault();
        if (user != null)
        {
          session.LastUsed = DateTime.UtcNow;
          this.mainContext.Sessions.Update(session);
        }
        else
        {
          this.mainContext.Sessions.Remove(session);
        }
        this.mainContext.SaveChanges();
      }

      return user;
    }

    /// <summary>
    /// Parses a passed token and returns the full
    /// user view model for that token.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public UserVM GetUserVMFromToken(string token)
    {
      var user = GetUserFromToken(token);
      UserVM userVM = null;

      if (user != null)
      {
        userVM = new UserService(mainContext, user).GetUser(user);
      }

      return userVM;
    }

    /// <summary>
    /// Creates a new session and returns that session
    /// along with it's token for a passed in login attempt.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Grants a specific security role to a passed in user
    /// provided the created by user is allowed to perform
    /// that action.
    /// </summary>
    /// <param name="UserId"></param>
    /// <param name="CreatedById"></param>
    /// <param name="RoleName"></param>
    public bool GrantRole(Guid UserId, Guid CreatedById, String RoleName)
    {
      bool created = false;
      var testRole = this.mainContext.AccessRoles.Where(r => r.UserId == UserId && r.Name == RoleName).FirstOrDefault();

      if (testRole == null)
      {
        created = true;
        var role = new AccessRole(UserId, RoleName);
        role.CreatedBy = CreatedById;
        this.mainContext.AccessRoles.Add(role);
        this.mainContext.SaveChanges();
      }

      return created;
    }

    public UserVM GetUserVMFromUsername(string userName, UserVM actor)
    {
      var user = this.mainContext.Users.Where(u => u.UserName == userName).FirstOrDefault();
      UserVM userVM = null;

      if (user != null)
      {
        userVM = new UserService(mainContext, actor).GetUser(user);
      }

      return userVM;
    }
  }
}