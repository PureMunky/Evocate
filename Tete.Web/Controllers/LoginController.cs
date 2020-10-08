using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Configuration;
using Tete.Models.Authentication;
using Tete.Web.Helpers;
using Tete.Web.Models;

namespace Tete.Api.Controllers
{
  public class LoginController : Controller
  {

    /* FIXME: Password reset flows
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
    private Tete.Api.Contexts.MainContext context;

    public LoginController(Tete.Api.Contexts.MainContext mainContext)
    {
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
      IActionResult direction = Redirect("/");
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
        SetTokenCookie(session.Token);
      }
      else
      {
        direction = View("Index", "Invalid Login");
      }

      return direction;
    }

    [HttpPost]
    public Response<RegistrationResponse> Delete([FromBody] LoginAttempt login)
    {
      var response = new RegistrationResponse()
      {
        Successful = false
      };
      var service = new Tete.Api.Services.Authentication.LoginService(this.context);
      var token = HttpContext.Request.Cookies[Constants.SessionTokenName];
      var user = service.GetUserVMFromToken(token);
      var session = service.Login(login);
      var user2 = service.GetUserVMFromToken(session.Token);

      if (user != null && session != null && user2 != null && user.UserId == user2.UserId)
      {
        try
        {
          service.DeleteAccount(user.UserId, user);
          response.Successful = true;
        }
        catch { }
      }

      if (!response.Successful)
      {
        response.Messages.Add("Unable to delete account due to login issues.");
      }

      return new Response<RegistrationResponse>(response);
    }

    [HttpPost]
    public Response<RegistrationResponse> AdminDelete([FromBody] RoleUpdate login)
    {
      var response = new RegistrationResponse()
      {
        Successful = false
      };
      var service = new Tete.Api.Services.Authentication.LoginService(this.context);
      var token = HttpContext.Request.Cookies[Constants.SessionTokenName];
      var user = service.GetUserVMFromToken(token);

      if (user != null)
      {
        try
        {
          service.DeleteAccount(login.UserId, user);
          response.Successful = true;
        }
        catch { }
      }

      if (!response.Successful)
      {
        response.Messages.Add("Failed to delete account.");
      }

      return new Response<RegistrationResponse>(response);
    }

    [HttpPost]
    public Response<RegistrationResponse> Login([FromBody] LoginAttempt login)
    {
      var service = new Tete.Api.Services.Authentication.LoginService(this.context);
      var token = HttpContext.Request.Cookies[Constants.SessionTokenName];
      var user = service.GetUserVMFromToken(token);
      var response = new RegistrationResponse();

      if (user == null || (user != null && user.Roles.Contains("Guest")))
      {
        var session = service.Login(login);

        if (session != null)
        {
          if (user != null)
          {
            service.DeleteAccount(user.UserId, user);
          }

          SetTokenCookie(session.Token);
          response.Successful = true;
        }
        else
        {
          response.Messages.Add("Invalid Login");
          response.Successful = false;
        }
      }
      else
      {
        response.Messages.Add("You're already logged in!");
        response.Successful = false;
      }

      return new Response<RegistrationResponse>(response);
    }

    [HttpGet]
    public Response<UserVM> GetUser(string userName)
    {
      var service = new Tete.Api.Services.Authentication.LoginService(this.context);
      return new Response<UserVM>(service.GetUserVMFromUsername(userName, CurrentUser()));
    }

    [HttpGet]
    public UserVM CurrentUser()
    {
      var service = new Tete.Api.Services.Authentication.LoginService(this.context);
      var token = HttpContext.Request.Cookies[Constants.SessionTokenName];

      var user = service.GetUserVMFromToken(token);

      if (user == null)
      {
        var session = service.GetNewAnonymousSession();
        SetTokenCookie(session.Token);

        user = service.GetUserVMFromToken(session.Token);
      }

      return user;
    }

    private void SetTokenCookie(string token)
    {
      HttpContext.Response.Cookies.Append(Constants.SessionTokenName, token, new Microsoft.AspNetCore.Http.CookieOptions()
      {
        HttpOnly = true,
        Expires = DateTime.Now.AddYears(Constants.AuthenticationCookieLifeYears),
        Secure = true
      });
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

    // [HttpPost]
    // public IActionResult Register(string userName, string userPassword, string userEmail, string userDisplayName)
    // {
    //   IActionResult direction = Redirect("/");

    //   var service = new Tete.Api.Services.Authentication.LoginService(this.context);
    //   var response = service.Register(
    //     new RegistrationAttempt()
    //     {
    //       UserName = userName,
    //       Password = userPassword,
    //       Email = userEmail,
    //       DisplayName = userDisplayName
    //     }
    //   );

    //   if (response.Successful)
    //   {
    //     var session = service.Login(new LoginAttempt()
    //     {
    //       UserName = userName,
    //       Password = userPassword
    //     });

    //     if (session != null)
    //     {
    //       SetTokenCookie(session.Token);
    //     }
    //     else
    //     {
    //       direction = View("Register");
    //     }
    //   }
    //   else
    //   {
    //     direction = View("Register", response);
    //   }

    //   return direction;
    // }

    [HttpPost]
    public Response<RegistrationResponse> ResetPassword([FromBody] LoginAttempt login)
    {
      var token = HttpContext.Request.Cookies[Constants.SessionTokenName];
      var service = new Tete.Api.Services.Authentication.LoginService(this.context);

      return new Response<RegistrationResponse>(service.ResetPassword(token, login.Password));
    }

    [HttpPost]
    public Response<RegistrationResponse> UpdateUserName([FromBody] LoginAttempt login)
    {
      var token = HttpContext.Request.Cookies[Constants.SessionTokenName];
      var service = new Tete.Api.Services.Authentication.LoginService(this.context);

      return new Response<RegistrationResponse>(service.UpdateUserName(token, login.UserName));
    }

    [HttpPost]
    public Response<RegistrationResponse> RegisterNewLogin([FromBody] LoginAttempt login)
    {
      var token = HttpContext.Request.Cookies[Constants.SessionTokenName];
      var service = new Tete.Api.Services.Authentication.LoginService(this.context);

      return new Response<RegistrationResponse>(service.RegisterNewLogin(token, login));
    }
  }
}