using Microsoft.AspNetCore.Mvc;
using Tete.Models.Authentication;

namespace Tete.Api.Controllers
{

  [Route("V1/[controller]/[action]")]
  [ApiController]
  public class LoginController : ControllerBase
  {
    private Api.Services.Authentication.LoginService service;
    private Api.Services.Logging.LogService logService;

    public LoginController(Contexts.MainContext mainContext)
    {
      this.service = new Services.Authentication.LoginService(mainContext);
      this.logService = new Services.Logging.LogService(mainContext, "Api");
    }

    [HttpPost]
    public SessionVM Login(LoginAttempt login)
    {
      this.logService.Write("Attempting Login", login.UserName);
      return this.service.Login(login);
    }

    [HttpPost]
    public SessionVM Register(RegistrationAttempt registration)
    {
      this.logService.Write("Registering User", registration.UserName);
      this.service.Register(registration);
      return this.service.Login(new LoginAttempt()
      {
        UserName = registration.UserName,
        Password = registration.Password
      });
    }

    [HttpGet]
    public UserVM CurrentUser()
    {
      var token = HttpContext.Request.Cookies["Tete.SessionToken"];
      return CurrentUser(token);
    }

    public UserVM CurrentUser(string token)
    {
      this.logService.Write("Getting Current User");
      return this.service.GetUserVMFromToken(token);
    }
  }
}