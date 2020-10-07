using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Tete.Api.Services.Localization;
using Tete.Api.Services.Authentication;
using Tete.Api.Services.Content;
using Tete.Models.Authentication;
using Tete.Models.Localization;
using Tete.Api.Services.Users;
using Tete.Api.Helpers;


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
    private TopicService topicService;

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

      this.mainContext.Migrate();

      var testAdminUser = this.mainContext.Users.Where(u => u.UserName == adminUserName).FirstOrDefault();
      if (testAdminUser == null)
      {
        var session = this.loginService.GetNewAnonymousSession();
        var response = this.loginService.RegisterNewLogin(session.Token, new LoginAttempt()
        {
          UserName = adminUserName,
          Password = "123admin!"
        });

        if (response.Successful)
        {
          output.Add("User 'Admin' created.");
        }
        else
        {
          output.AddRange(response.Messages);
        }

        adminUser = this.loginService.GetUserFromToken(session.Token);
      }
      else
      {
        output.Add("User 'Admin' already existed.");

        adminUser = testAdminUser;
      }


      if (this.mainContext.AccessRoles.Where(ar => ar.UserId == adminUser.Id && ar.Name == "Admin").FirstOrDefault() == null)
      {
        this.mainContext.AccessRoles.Add(new AccessRole(adminUser.Id, "Admin"));
        this.mainContext.SaveChanges();
      }

      var adminUserVM = new UserService(mainContext, adminUser).GetUser(adminUser);

      this.languageService = new LanguageService(this.mainContext, adminUserVM);
      this.topicService = new TopicService(this.mainContext, adminUserVM);

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

      var supportKeyword = new Models.Content.Keyword()
      {
        Name = "support",
        Restricted = true
      };

      List<Tete.Models.Content.TopicVM> SetupTopics = new List<Models.Content.TopicVM>()
      {
        new Models.Content.TopicVM()
        {
          Name = "Tête General Questions",
          Description = "Ask any general questions you have about Tete here.",
          Elligible = true,
          Keywords = new List<Models.Content.Keyword>() {
            supportKeyword
          }
        },
        new Models.Content.TopicVM()
        {
          Name = "Tête Issues",
          Description = "If you're experiencing an error and need help resolving it the reqest a mentor for this topic.",
          Elligible = true,
          Keywords = new List<Models.Content.Keyword>() {
            supportKeyword
          }
        },
        new Models.Content.TopicVM()
        {
          Name = "Donate to Tête",
          Description = "If you like what we've got going on here help us out with a few bucks to cover server costs by clicking the \"Donate\" link.",
          Elligible = true,
          Keywords = new List<Models.Content.Keyword>() {
            supportKeyword
          },
          Links = new List<Models.Content.Link>() {
            new Models.Content.Link() {
              Destination = "https://paypal.me/philcorbettlive?locale.x=en_US",
              Name = "Donate"
            }
          }
        }
      };

      foreach (Tete.Models.Content.TopicVM t in SetupTopics)
      {
        topicService.SaveTopic(t);
        output.Add(string.Format("Created {0} Topic", t.Name));
      }

      return output;
    }
  }
}