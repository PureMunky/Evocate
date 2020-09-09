using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Configuration;
using Tete.Models.Authentication;
using Tete.Web.Helpers;

namespace Tete.Web.Controllers
{
  public class LoginController : Controller
  {

    /* TODO: Password reset flows
       1. user guided
       - magic link that a user should bookmark
       - enter their username to be allowed to reset
       - prompted for a new password then logged in.
       2. admin one-click password reset
       - admin enters username
       - provided with magic link and email address
       - send link to email address
       3. reset password email (ideal, requires smtp)
       - user click "reset" on login screen
       - sent an email with magic link
    */
    private IConfiguration Configuration;
    private Tete.Api.Contexts.MainContext context;

    public LoginController(IConfiguration configuration, Tete.Api.Contexts.MainContext mainContext)
    {
      Configuration = configuration;
      this.context = mainContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
      var user = CurrentUser();
      if (user != null)
      {
        return Redirect("/");
      }
      else
      {
        return View();
      }

    }

    [HttpPost]
    public IActionResult Index(string userName, string userPassword)
    {
      string direction = "/";
      var service = new Tete.Api.Services.Authentication.LoginService(this.context);
      var session = service.Login(
        new LoginAttempt()
        {
          UserName = userName,
          Password = userPassword
        }
      );

      if (session != null)
      {
        HttpContext.Response.Cookies.Append(Constants.SessionTokenName, session.Token, new Microsoft.AspNetCore.Http.CookieOptions()
        {
          HttpOnly = true,
          Expires = DateTime.Now.AddYears(Constants.AuthenticationCookieLifeYears),
          Secure = true
        });
      }
      else
      {
        direction = "/Login";
      }


      return Redirect(direction);
    }

    [HttpGet]
    public UserVM CurrentUser()
    {
      HttpContext.Response.StatusCode = 401;

      var service = new Tete.Api.Services.Authentication.LoginService(this.context);
      var token = HttpContext.Request.Cookies[Constants.SessionTokenName];

      var user = service.GetUserVMFromToken(token);

      if (user != null)
      {
        HttpContext.Response.StatusCode = 200;
      }

      return user;
    }

    [HttpGet]
    public IActionResult Logout()
    {
      var token = HttpContext.Request.Cookies[Constants.SessionTokenName];
      var service = new Tete.Api.Services.Authentication.LoginService(this.context);
      service.Logout(token);

      HttpContext.Response.Cookies.Delete(Constants.SessionTokenName);

      return Redirect("/Login");
    }

    public IActionResult Forgot()
    {
      return View("Forgot");
    }

    [HttpGet]
    public IActionResult Register()
    {
      return View("Register");
    }

    [HttpPost]
    public IActionResult Register(string userName, string userPassword, string userEmail, string userDisplayName)
    {
      string direction = "/";

      var service = new Tete.Api.Services.Authentication.LoginService(this.context);
      service.Register(
        new RegistrationAttempt()
        {
          UserName = userName,
          Password = userPassword,
          Email = userEmail,
          DisplayName = userDisplayName
        }
      );

      var session = service.Login(new LoginAttempt()
      {
        UserName = userName,
        Password = userPassword
      });

      if (session != null)
      {
        HttpContext.Response.Cookies.Append(Constants.SessionTokenName, session.Token, new Microsoft.AspNetCore.Http.CookieOptions()
        {
          HttpOnly = true,
          Expires = DateTime.Now.AddYears(Constants.AuthenticationCookieLifeYears),
          Secure = true
        });
      }
      else
      {
        direction = "/Login";
      }

      return Redirect(direction);
    }

    [HttpPost]
    public void Reset(string newPassword)
    {
      // TODO: Fix non escaped characters in new passwords.
      var token = HttpContext.Request.Cookies[Constants.SessionTokenName];
      var service = new Tete.Api.Services.Authentication.LoginService(this.context);

      service.ResetPassword(token, newPassword);
    }

  }
}