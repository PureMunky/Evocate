using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Configuration;
using Tete.Models.Authentication;
using Tete.Web.Helpers;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tete.Web.Controllers
{
  public class LoginController : Controller
  {

    private IConfiguration Configuration;

    public LoginController(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    [HttpGet]
    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string userName, string userPassword)
    {
      string direction = "/";
      SessionVM session = await new Tete.Web.Services.RequestService(Configuration)
      .Post<LoginAttempt, SessionVM>(
        "/v1/Login/Login",
        new LoginAttempt()
        {
          UserName = userName,
          Password = userPassword
        },
        HttpContext
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
    public async Task<dynamic> CurrentUser()
    {
      return await new Tete.Web.Services.RequestService(Configuration).Get("/v1/Login/CurrentUser", HttpContext);
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
    public async Task<IActionResult> Register(string userName, string userPassword, string userEmail, string userDisplayName)
    {
      string direction = "/";
      SessionVM session = await new Tete.Web.Services.RequestService(Configuration)
      .Post<RegistrationAttempt, SessionVM>(
        "/v1/Login/Register",
        new RegistrationAttempt()
        {
          UserName = userName,
          Password = userPassword,
          Email = userEmail,
          DisplayName = userDisplayName
        },
        HttpContext
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
  }
}