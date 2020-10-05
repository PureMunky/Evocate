using Microsoft.EntityFrameworkCore;
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

    #region Private Properties

    private MainContext mainContext;

    #endregion

    #region Public Properties

    public Tete.Api.Services.Logging.LogService LogService
    {
      get
      {
        return new Logging.LogService(this.mainContext, Tete.Api.Services.Logging.LogService.LoggingLayer.Service, null);
      }
    }

    #endregion

    #region Constructor

    public LoginService(MainContext mainContext)
    {
      this.mainContext = mainContext;
    }

    #endregion

    #region Public Functions

    public SessionVM Login(LoginAttempt login)
    {
      LogService.Write("Login", login.UserName);
      return GetNewToken(login);
    }

    public void Logout(string Token)
    {
      var session = this.mainContext.Sessions.Where(s => s.Token == Token).FirstOrDefault();
      if (session != null)
      {
        LogService.Write("Logout", String.Format("User:{0}", session.UserId));
        this.mainContext.Sessions.Remove(session);
        this.mainContext.SaveChanges();
      }
    }

    public User GetUserFromToken(string token)
    {
      var session = this.mainContext.Sessions.SingleOrDefault(s => s.Token == token);

      User user = null;
      if (session != null)
      {
        user = this.mainContext.Users.AsNoTracking().Where(u => u.Id == session.UserId).FirstOrDefault();
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

    public SessionVM GetNewAnonymousSession()
    {
      var user = RegisterUser(new RegistrationAttempt()
      {
        UserName = "",
        DisplayName = "Guest",
        Email = ""
      });

      new UserService(this.mainContext, new UserVM(user)).GrantGuestRole(user.Id);

      var session = CreateNewSession(user);

      return new SessionVM(session);
    }



    public UserVM GetUserVMFromUsername(string userName, UserVM actor)
    {
      UserVM userVM = null;

      if (userName != "")
      {
        var user = this.mainContext.Users.Where(u => u.UserName == userName).FirstOrDefault();

        if (user != null)
        {
          userVM = new UserService(mainContext, actor).GetUser(user);
        }
      }
      return userVM;
    }

    public RegistrationResponse ResetPassword(string token, string newPassword)
    {
      var rtnResponse = ValidatePassword(newPassword);

      if (rtnResponse.Successful)
      {
        var user = GetUserFromToken(token);

        if (user != null)
        {
          UpdatePassword(user.Id, newPassword, user.Salt);
        }
      }

      return rtnResponse;
    }

    public RegistrationResponse UpdateUserName(string token, string newUserName)
    {
      var rtnResponse = ValidateUserName(newUserName);

      if (rtnResponse.Successful)
      {
        var user = GetUserFromToken(token);

        if (user != null)
        {
          var dbUser = this.mainContext.Users.Where(u => u.Id == user.Id).FirstOrDefault();
          if (dbUser != null)
          {
            dbUser.UserName = newUserName;
            this.mainContext.Users.Update(dbUser);
            this.mainContext.SaveChanges();
          }
        }
      }

      return rtnResponse;
    }

    public RegistrationResponse RegisterNewLogin(string token, LoginAttempt login)
    {
      var rtnResponse = ValidatePassword(login.Password);
      rtnResponse.Combine(ValidateUserName(login.UserName));

      if (rtnResponse.Successful)
      {
        var user = GetUserFromToken(token);

        if (user != null)
        {
          var dbUser = this.mainContext.Users.Where(u => u.Id == user.Id).FirstOrDefault();
          if (dbUser != null)
          {
            UpdatePassword(user.Id, login.Password, user.Salt);
            dbUser.UserName = login.UserName;
            this.mainContext.Users.Update(dbUser);
            this.mainContext.SaveChanges();
            new UserService(this.mainContext, new UserVM(user)).RemoveGuestRole(user.Id);
          }

        }
      }

      return rtnResponse;
    }

    public void DeleteAccount(Guid UserId, UserVM Actor)
    {
      if (UserId == Actor.UserId || Actor.Roles.Contains("Admin"))
      {
        var user = this.mainContext.Users.Single(u => u.Id == UserId);
        DeleteUser(user);
      }

    }

    private void DeleteUser(User user)
    {
      this.mainContext.AccessRoles.RemoveRange(this.mainContext.AccessRoles.Where(ar => ar.UserId == user.Id));
      this.mainContext.Logins.RemoveRange(this.mainContext.Logins.Where(l => l.UserId == user.Id));
      this.mainContext.Sessions.RemoveRange(this.mainContext.Sessions.Where(s => s.UserId == user.Id));
      this.mainContext.UserLanguages.RemoveRange(this.mainContext.UserLanguages.Where(ul => ul.UserId == user.Id));
      this.mainContext.UserProfiles.RemoveRange(this.mainContext.UserProfiles.Where(up => up.UserId == user.Id));
      this.mainContext.UserTopics.RemoveRange(this.mainContext.UserTopics.Where(ut => ut.UserId == user.Id));
      this.mainContext.UserBlocks.RemoveRange(this.mainContext.UserBlocks.Where(ub => ub.UserId == user.Id));

      this.mainContext.Users.Remove(user);

      this.mainContext.SaveChanges();
    }
    #endregion

    #region Private Functions

    private User RegisterUser(RegistrationAttempt registration)
    {
      byte[] salt = Crypto.NewSalt();
      var newUser = new User()
      {
        UserName = registration.UserName,
        Email = registration.Email,
        DisplayName = registration.DisplayName,
        Salt = salt
      };

      LogService.Write("Register", String.Format("User:{0}", newUser.Id));
      this.mainContext.Users.Add(newUser);
      this.mainContext.SaveChanges();

      return newUser;
    }

    private void UpdatePassword(Guid UserId, string Password, byte[] Salt)
    {
      string hash = Crypto.Hash(Password, Salt);
      var dbLogin = this.mainContext.Logins.Where(l => l.UserId == UserId).OrderByDescending(l => l.Created).FirstOrDefault();

      if (dbLogin == null)
      {
        var newLogin = new Login()
        {
          UserId = UserId,
          PasswordHash = hash
        };
        this.mainContext.Logins.Add(newLogin);
      }
      else
      {
        dbLogin.PasswordHash = hash;
        this.mainContext.Logins.Update(dbLogin);
      }

      LogService.Write("Updated Password", String.Format("User:{0};", UserId));
      this.mainContext.SaveChanges();
    }

    private SessionVM GetNewToken(LoginAttempt login)
    {
      SessionVM sessionVM = null;
      Session session = null;

      if (login.UserName != null && login.UserName != "")
      {
        var user = this.mainContext.Users.Where(u => u.UserName == login.UserName).FirstOrDefault();
        LogService.Write("NewToken Attempt", String.Format("User:{0}", login.UserName));

        if (user != null)
        {
          string hash = Crypto.Hash(login.Password, user.Salt);

          var dbLogin = this.mainContext.Logins.Where(l => l.PasswordHash == hash && l.UserId == user.Id).FirstOrDefault();
          if (dbLogin != null)
          {
            session = CreateNewSession(user);
          }
        }

        if (session != null)
        {
          sessionVM = new SessionVM(session);
        }
      }

      return sessionVM;
    }

    private Session CreateNewSession(User user)
    {
      string token = Crypto.Hash(Guid.NewGuid().ToString() + user.Id, user.Salt);
      var session = new Session()
      {
        UserId = user.Id,
        Token = token
      };

      LogService.Write("NewToken", String.Format("User:{0}", user.Id));

      this.mainContext.Sessions.Add(session);
      this.mainContext.SaveChanges();
      return session;
    }



    private RegistrationResponse ValidatePassword(string password)
    {
      var rtnResponse = new RegistrationResponse();

      if (password == null)
      {
        password = "";
      }

      if (password.Length < 8)
      {
        rtnResponse.Messages.Add("Password must be 8 or more characters.");
        rtnResponse.Successful = false;
      }

      var special = "!@#$%^&()?";
      if (!password.ToCharArray().Any(c => special.Contains(c)))
      {
        rtnResponse.Messages.Add(String.Format("Password must contain a special character ({0}).", special));
        rtnResponse.Successful = false;
      }

      var numbers = "0123456789";
      if (!password.ToCharArray().Any(c => numbers.Contains(c)))
      {
        rtnResponse.Messages.Add(String.Format("Password must contain a special character ({0}).", numbers));
        rtnResponse.Successful = false;
      }

      return rtnResponse;
    }

    private RegistrationResponse ValidateUserName(string UserName)
    {
      var response = new RegistrationResponse();

      if (UserName == null)
      {
        UserName = "";
      }

      if (UserName.Length <= 0)
      {
        response.Messages.Add("UserName too short");
        response.Successful = false;
      }

      if (response.Successful && this.mainContext.Users.Where(u => u.UserName == UserName).Count() > 0)
      {
        response.Messages.Add("UserName already taken");
        response.Successful = false;
      }

      return response;
    }

    #endregion

  }
}