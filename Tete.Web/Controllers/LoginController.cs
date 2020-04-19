using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Configuration;
using Tete.Models.Authentication;
using Tete.Web.Helpers;

namespace Tete.Web.Controllers
{
  public class LoginController : Controller
  {

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
      return View();
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
          Expires = DateTime.Now.AddMinutes(Constants.AuthenticationCookieLife),
          // Secure = true
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
      var token = HttpContext.Request.Cookies["Tete.SessionToken"];

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
          Expires = DateTime.Now.AddMinutes(Constants.AuthenticationCookieLife),
          // Secure = true
        });
      }
      else
      {
        direction = "/Login";
      }


      return Redirect(direction);
    }
  }
}