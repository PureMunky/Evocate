using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using Tete.Models.Authentication;
using Tete.Web.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
          string Url = Configuration["Tete:ApiEndpoint"] + "/v1/Login/Login";
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
          Console.Write(e.Message);
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
    public async Task<dynamic> CurrentUser()
    {
      dynamic user = null;
      var cookieContainer = new CookieContainer();

      using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
      {
        using (var client = new HttpClient(handler))
        {
          try
          {
            Uri Url = new Uri(Configuration["Tete:ApiEndpoint"] + "/v1/Login/CurrentUser");
            cookieContainer.Add(Url, new Cookie(Constants.SessionTokenName, HttpContext.Request.Cookies[Constants.SessionTokenName]));

            HttpResponseMessage res = await client.GetAsync(Url);
            string content = await res.Content.ReadAsStringAsync();
            user = JsonConvert.DeserializeObject(content);
          }
          catch (Exception e)
          {
            Console.Write(e.Message);
          }
        }

        if (!HttpContext.Request.Cookies.Keys.Contains(Constants.SessionTokenName))
        {
          HttpContext.Response.StatusCode = 401;
        }
        else
        {
          HttpContext.Response.StatusCode = 200;
        }
      }
      return user;
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
    public async Task<IActionResult> Register(string userEmail, string userPassword)
    {
      string token = "";
      using (var client = new HttpClient())
      {
        try
        {
          string Url = Configuration["Tete:ApiEndpoint"] + "/v1/Login/Register";
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
  }
}