using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tete.Api.Services.Localization;
using Tete.Api.Services.Authentication;
using Tete.Models.Authentication;
using Tete.Models.Localization;
using Tete.Api.Services.Users;


namespace Tete.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class InitController : ControllerBase
  {
    private Contexts.MainContext mainContext;
    private LoginController loginController;
    private LanguageService languageService;
    private LoginService loginService;

    public InitController(Contexts.MainContext mainContext)
    {
      this.mainContext = mainContext;
      this.loginController = new LoginController(mainContext);
      this.loginService = new LoginService(mainContext);
    }
    // GET api/values
    [HttpGet]
    public List<string> Get()
    {
      // TODO: Allow this to be run multiple times without error.
      var output = new List<string>();
      var session = this.loginController.Register(new RegistrationAttempt()
      {
        UserName = "admin",
        Email = "admin@example.com",
        Password = "admin",
        DisplayName = "Admin"
      });
      output.Add("User 'Admin' created.");

      var adminuser = this.loginService.GetUserFromToken(session.Token);


      this.loginService.GrantRole(adminuser.Id, adminuser.Id, "Admin");
      output.Add("Granted Admin Role to Admin User.");

      var adminUserVM = new UserService(mainContext, adminuser).GetUser(adminuser);

      this.languageService = new LanguageService(this.mainContext, adminUserVM);

      var english = new Language()
      {
        LanguageId = Guid.NewGuid(),
        Name = "English",
        Active = true,
        Elements = new List<Element>()
      };

      english.Elements.Add(new Element()
      {
        ElementId = Guid.NewGuid(),
        Key = "welcome",
        Text = "Welcome!",
        LanguageId = english.LanguageId
      });

      this.languageService.CreateLanguage(english);
      output.Add("Created English Language");

      return output;
    }
  }
}