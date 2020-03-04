using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


namespace Tete.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class InitController : ControllerBase
  {
    private LoginController loginController;

    public InitController(Contexts.MainContext mainContext)
    {
      this.loginController = new LoginController(mainContext);
    }
    // GET api/values
    [HttpGet]
    public List<string> Get()
    {
      var output = new List<string>();
      this.loginController.Register(new Models.Authentication.RegistrationAttempt()
      {
        UserName = "admin",
        Email = "admin@example.com",
        Password = "admin",
        DisplayName = "Admin"
      });
      output.Add("User 'Admin' created.");

      return output;
    }
  }
}