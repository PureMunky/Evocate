using System;
using System.Collections.Generic;
using System.Linq;
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
      var output = new List<string>();
      var adminUserName = "admin";
      User adminUser;

      var testAdminUser = this.mainContext.Users.Where(u => u.UserName == adminUserName).FirstOrDefault();
      if (testAdminUser == null)
      {
        var session = this.loginController.Register(new RegistrationAttempt()
        {
          UserName = adminUserName,
          Email = "admin@example.com",
          Password = "admin",
          DisplayName = "Admin"
        });
        output.Add("User 'Admin' created.");

        adminUser = this.loginService.GetUserFromToken(session.Token);
      }
      else
      {
        output.Add("User 'Admin' already existed.");

        adminUser = testAdminUser;
      }


      if (this.loginService.GrantRole(adminUser.Id, adminUser.Id, "Admin"))
      {
        output.Add("Granted admin role to admin user.");
      }
      else
      {
        output.Add("Admin role already granted.");
      }

      var adminUserVM = new UserService(mainContext, adminUser).GetUser(adminUser);

      this.languageService = new LanguageService(this.mainContext, adminUserVM);

      var testLang = this.mainContext.Languages.Where(l => l.Name == "English").FirstOrDefault();

      if (testLang == null)
      {
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
        output.Add("Created English language");
      }
      else
      {
        output.Add("English language already existed.");
      }


      return output;
    }
  }
}