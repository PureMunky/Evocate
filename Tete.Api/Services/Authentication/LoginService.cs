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

    public string Login(LoginAttempt login)
    {
      // Select UserId from login where passwordhash = login.Password
      // Select true from user where userId = UserId and email = login.Email
      byte[] salt = Crypto.NewSalt();
      string hash = Crypto.Hash(login.Password, salt);
      // this.mainContext.Logins.Find(login.PasswordHash);
      return hash;
    }
  }
}