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
    public string Login(LoginAttempt login)
    {
      this.logService.Write("Attempting Login", login.Email);
      return this.service.Login(login);
    }

    [HttpPost]
    public string Register(LoginAttempt login)
    {
      this.logService.Write("Registering User", login.Email);
      return this.service.Register(login);
    }

    [HttpGet]
    public User GetUser()
    {
      this.logService.Write("Getting Current User");
      var token = HttpContext.Request.Cookies["Tete.SessionToken"];
      return this.service.GetUserFromToken(token);
    }
  }
}