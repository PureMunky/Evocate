using System;
using System.Linq;
using System.Collections.Generic;
using Tete.Api.Contexts;
using Tete.Api.Services.Localization;
using Tete.Models.Content;
using Tete.Models.Authentication;

namespace Tete.Api.Services.Content
{
  public class TopicService : ServiceBase
  {

    private UserLanguageService userLanguageService;
    private Logging.LogService logService;

    public TopicService(MainContext mainContext, UserVM actor)
    {
      FillData(mainContext, actor);
    }

    public void SaveTopic(TopicVM topic)
    {
      // var prof = this.mainContext.UserProfiles.Where(p => p.ProfileId == profile.ProfileId).FirstOrDefault();

      // if (prof is null)
      // {
      //   this.mainContext.Topic.Add(profile);
      // }
      // else
      // {
      //   if (
      //     prof.UserId == this.Actor.UserId
      //     || this.Actor.Roles.Contains("Admin")
      //     )
      //   {
      //     prof.About = profile.About;
      //     prof.PrivateAbout = profile.PrivateAbout;
      //     this.mainContext.UserProfiles.Update(prof);
      //   }
      // }
      // this.mainContext.SaveChanges();
    }

    public IEnumerable<TopicVM> Search(string searchText)
    {
      return new List<Topic>() {
        new Topic() {
          Name = "Test",
          Description = "Hello I'm testing this out."
        },
        new Topic() {
          Name = "Being Amazing",
          Description = "So you want to learn to be amazing?!?"
        }
      }.Select(t => new TopicVM(t));
    }

    private void FillData(MainContext mainContext, UserVM actor)
    {
      this.mainContext = mainContext;
      this.Actor = actor;
      this.userLanguageService = new UserLanguageService(mainContext, actor);
      this.logService = new Logging.LogService(mainContext, Logging.LogService.LoggingLayer.Api);
    }
  }
}