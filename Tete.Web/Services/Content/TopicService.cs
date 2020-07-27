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
      var dbTopic = this.mainContext.Topics.Where(t => t.TopicId == topic.TopicId).FirstOrDefault();

      if (dbTopic is null)
      {
        var newTopic = new Topic();

        newTopic.Name = topic.Name;
        newTopic.Description = topic.Description;
        newTopic.Created = DateTime.UtcNow;
        newTopic.CreatedBy = this.Actor.UserId;
        newTopic.Elligible = false;
        newTopic.TopicId = Guid.NewGuid();

        this.mainContext.Topics.Add(newTopic);
      }
      else
      {
        if (this.Actor.Roles.Contains("Admin"))
        {
          dbTopic.Name = topic.Name;
          dbTopic.Description = topic.Description;
          this.mainContext.Topics.Update(dbTopic);
        }
      }
      this.mainContext.SaveChanges();
    }

    public IEnumerable<TopicVM> Search(string searchText)
    {
      searchText = searchText.ToLower();

      return this.mainContext.Topics.Where(t => t.Name.ToLower().Contains(searchText) || t.Description.ToLower().Contains(searchText)).Select(t => new TopicVM(t));
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