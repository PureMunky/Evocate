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