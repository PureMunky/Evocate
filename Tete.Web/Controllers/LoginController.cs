using Microsoft.AspNetCore.Mvc;
using System;
using Tete.Web.Helpers;

namespace Tete.Web.Controllers
{
  public class LoginController : Controller
  {
    [HttpGet]
    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Index(string userEmail, string userPassword)
    {
      HttpContext.Response.Cookies.Append(Constants.SessionTokenName, userEmail, new Microsoft.AspNetCore.Http.CookieOptions()
      {
        HttpOnly = true,
        Expires = DateTime.Now.AddMinutes(Constants.AuthenticationCookieLife),
        // Secure = true
      });

      return Redirect("/");
    }

    [HttpGet]
    public void Test()
    {
      if (!HttpContext.Request.Cookies.Keys.Contains(Constants.SessionTokenName))
      {
        HttpContext.Response.StatusCode = 401;
      }
      else
      {
        HttpContext.Response.StatusCode = 200;
      }

    }

    public IActionResult Forgot()
    {
      return View("Forgot");
    }

    public IActionResult Register()
    {
      return View("Register");
    }
  }
}