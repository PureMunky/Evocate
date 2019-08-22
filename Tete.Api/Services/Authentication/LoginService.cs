using Tete.Api.Contexts;
using Tete.Models.Authentication;

namespace Tete.Api.Services.Authentication
{
  public class LoginService
  {
    private MainContext mainContext;

    public LoginService(MainContext mainContext)
    {
      this.mainContext = mainContext;
    }

    public string Login(Login login)
    {
      // Select UserId from login where passwordhash = login.Password
      // Select true from user where userId = UserId and email = login.Email
      return "";
    }
  }
}