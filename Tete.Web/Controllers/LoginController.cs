using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Configuration;
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
    public async Task<IActionResult> Index(string userEmail, string userPassword)
    {
      string token = "";
      using (var client = new HttpClient())
      {
        try
        {
          string Url = Configuration["Tete:ApiEndpoint"] + "/v1/Login";
          HttpResponseMessage res = await client.PostAsJsonAsync(Url,
          new Tete.Models.Authentication.LoginAttempt()
          {
            Email = userEmail,
            Password = userPassword
          });
          token = await res.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {

        }
      }

      HttpContext.Response.Cookies.Append(Constants.SessionTokenName, token, new Microsoft.AspNetCore.Http.CookieOptions()
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