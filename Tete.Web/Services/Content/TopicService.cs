using System;
using System.Linq;
using System.Collections.Generic;
using Tete.Api.Contexts;
using Tete.Api.Services.Localization;
using Tete.Models.Content;
using Tete.Models.Authentication;
using Tete.Models.Relationships;

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

        if (topic.TopicId == Guid.Empty)
        {
          newTopic.TopicId = Guid.NewGuid();
        }
        else
        {
          newTopic.TopicId = topic.TopicId;
        }
        newTopic.Name = topic.Name;
        newTopic.Description = topic.Description;
        newTopic.Created = DateTime.UtcNow;
        newTopic.CreatedBy = this.Actor.UserId;
        newTopic.Elligible = false;

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

    public TopicVM GetTopic(Guid topicId)
    {
      var dbTopic = this.mainContext.Topics.Where(t => t.TopicId == topicId).FirstOrDefault();
      TopicVM rtnTopic = new TopicVM();

      if (dbTopic != null)
      {
        var dbUserTopic = this.mainContext.UserTopics.Where(ts => ts.UserId == this.Actor.UserId && ts.TopicId == topicId).FirstOrDefault();
        rtnTopic = new TopicVM(dbTopic);
        rtnTopic.UserTopic = dbUserTopic;

        if (dbUserTopic != null && rtnTopic.UserTopic.Status == TopicStatus.Mentor)
        {
          rtnTopic.Mentorships = this.mainContext.Mentorships.Where(m => m.Active == true && m.TopicId == topicId && (m.MentorUserId == this.Actor.UserId || m.MentorUserId == Guid.Empty)).Select(m => new MentorshipVM(m, null)).ToList();
        }
      }

      return rtnTopic;
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