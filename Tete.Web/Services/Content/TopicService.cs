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
        rtnTopic = new TopicVM(dbTopic, dbUserTopic);

        if (dbUserTopic != null && rtnTopic.UserTopic.Status == TopicStatus.Mentor)
        {
          rtnTopic.Mentorships = this.mainContext.Mentorships.Where(m => m.Active == true && m.TopicId == topicId && (m.MentorUserId == this.Actor.UserId || m.MentorUserId == Guid.Empty) && m.LearnerUserId != this.Actor.UserId).Select(m => new MentorshipVM(m, null)).ToList();
        }
      }

      return rtnTopic;
    }

    public IEnumerable<TopicVM> GetUserTopics(Guid UserId)
    {
      return this.mainContext.UserTopics
        .Where(ut => ut.UserId == UserId)
        .Join(this.mainContext.Topics, ut => ut.TopicId, t => t.TopicId, (ut, t) => new TopicVM(t, ut))
        .OrderByDescending(tv => tv.UserTopic.Status)
        .ThenBy(tv => tv.Name);
    }

    public IEnumerable<TopicVM> GetTopTopics()
    {
      var startDate = DateTime.UtcNow.AddMonths(-1);
      var dbMentorshipCounts = this.mainContext.Mentorships.Where(m => m.CreatedDate >= startDate).GroupBy(m => m.TopicId).Select(m => new { topicId = m.Key, count = m.Count() });
      var dbUserTopicCounts = this.mainContext.UserTopics.Where(ut => ut.CreatedDate >= startDate).GroupBy(ut => ut.TopicId).Select(g => new { topicId = g.Key, count = g.Count() });
      var topicIds = dbMentorshipCounts.Join(dbUserTopicCounts, m => m.topicId, ut => ut.topicId, (m, ut) => new { topicId = m.topicId, count = m.count + ut.count }).OrderByDescending(t => t.count);
      var rtnTopics = new List<TopicVM>();

      if (topicIds.Count() >= 10)
      {
        foreach (var t in topicIds.Take(10))
        {
          rtnTopics.Add(GetTopic(t.topicId));
        }
      }

      return rtnTopics;
    }

    public IEnumerable<TopicVM> GetNewestTopics()
    {
      var count = 10;
      var dbTopics = this.mainContext.Topics.OrderByDescending(t => t.Created).Take(count).Select(t => new TopicVM(t));

      return dbTopics;
    }

    public IEnumerable<TopicVM> GetWaitingTopics()
    {
      var count = 10;
      var dbTopics = this.mainContext.Mentorships
        .Where(m => m.MentorUserId == Guid.Empty)
        .GroupBy(m => m.TopicId)
        .Select(g => new { topicId = g.Key, count = g.Count() })
        .OrderByDescending(g => g.count)
        .Join(this.mainContext.Topics, g => g.topicId, t => t.TopicId, (g, t) => new TopicVM(t) { OpenMentorships = g.count })
        .Take(count);

      return dbTopics;
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