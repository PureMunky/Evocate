using Microsoft.AspNetCore.Mvc;
using Tete.Models.Authentication;
using Tete.Web.Models;
using Tete.Web.Helpers;

namespace Tete.Api.Controllers
{

  [Route("V1/[controller]/[action]")]
  [ApiController]
  public class LoginController : ControllerBase
  {
    private Api.Services.Authentication.LoginService service;

    public LoginController(Contexts.MainContext mainContext)
    {
      this.service = new Services.Authentication.LoginService(mainContext);
    }

    [HttpPost]
    public SessionVM Login(LoginAttempt login)
    {
      return this.service.Login(login);
    }

    [HttpPost]
    public SessionVM Register(RegistrationAttempt registration)
    {
      this.service.Register(registration);
      return this.service.Login(new LoginAttempt()
      {
        UserName = registration.UserName,
        Password = registration.Password
      });
    }

    [HttpGet]
    public Response<UserVM> GetUser(string userName)
    {
      var token = HttpContext.Request.Cookies[Constants.SessionTokenName];
      var actor = CurrentUser(token);
      return new Response<UserVM>(this.service.GetUserVMFromUsername(userName, actor));
    }

    [HttpGet]
    public UserVM CurrentUser()
    {
      var token = HttpContext.Request.Cookies[Constants.SessionTokenName];
      return CurrentUser(token);
    }

    public UserVM CurrentUser(string token)
    {
      return this.service.GetUserVMFromToken(token);
    }
  }
}