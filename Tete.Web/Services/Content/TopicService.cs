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

    public TopicVM SaveTopic(TopicVM topic)
    {
      var dbTopic = GetTopic(topic.TopicId);
      var dbSameNameCount = this.mainContext.Topics.Where(t => t.Name.ToLower() == topic.Name.ToLower() && t.TopicId != topic.TopicId).Count();
      Guid TopicId = Guid.Empty;

      if (dbSameNameCount == 0)
      {
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
          newTopic.CreatedBy = this.Actor.UserId;

          if (this.Actor.Roles.Contains("Admin"))
          {
            newTopic.Elligible = topic.Elligible;
          }

          TopicId = newTopic.TopicId;
          this.mainContext.Topics.Add(newTopic);
        }
        else if (this.Actor.Roles.Contains("Admin"))
        {
          dbTopic.Name = topic.Name;
          dbTopic.Description = topic.Description;
          dbTopic.Elligible = topic.Elligible;
          TopicId = dbTopic.TopicId;
          this.mainContext.Topics.Update(dbTopic);
        }

        if (this.Actor.Roles.Contains("Admin") && TopicId != Guid.Empty)
        {
          SaveKeywords(topic.Keywords, TopicId);
          SaveLinks(topic.Links, TopicId);
        }

        this.mainContext.SaveChanges();
      }

      return GetTopicVM(topic.TopicId);
    }

    public IEnumerable<TopicVM> Search(string searchText)
    {
      searchText = searchText.ToLower();

      return this.mainContext.Topics.Where(t => t.Name.ToLower().Contains(searchText) || t.Description.ToLower().Contains(searchText)).Select(t => new TopicVM(t));
    }

    public IEnumerable<TopicVM> GetKeywordTopics(string keyword)
    {
      keyword = keyword.ToLower();
      return this.mainContext.TopicKeywords
        .Where(tk => tk.Keyword.Name.ToLower() == keyword)
        .Join(this.mainContext.Topics, tk => tk.TopicId, t => t.TopicId, (tk, t) => new TopicVM(t))
        .ToList();
    }

    public TopicVM GetTopicVM(Guid topicId)
    {
      var dbTopic = GetTopic(topicId);
      TopicVM rtnTopic = new TopicVM();

      if (dbTopic != null)
      {

        var dbUserTopic = GetUserTopics(this.Actor.UserId, topicId).FirstOrDefault();
        rtnTopic = new TopicVM(dbTopic, dbUserTopic);

        rtnTopic.Links = this.mainContext.Links.Where(l => l.Active).Join(this.mainContext.TopicLinks.Where(tl => tl.TopicId == rtnTopic.TopicId && tl.Active), l => l.LinkId, tl => tl.LinkId, (l, tl) => l).OrderBy(l => l.Name).ToList();
        rtnTopic.Keywords = this.mainContext.Keywords.Where(k => k.Active).Join(this.mainContext.TopicKeywords.Where(tk => tk.TopicId == rtnTopic.TopicId), k => k.KeywordId, tk => tk.KeywordId, (k, tk) => k).OrderBy(l => l.Name).ToList();

        if (dbUserTopic != null && rtnTopic.UserTopic.Status == TopicStatus.Mentor)
        {
          rtnTopic.Mentorships = MentorshipService.OpenMentorships(this.Actor.UserId, topicId).Select(m => new MentorshipVM(m, null)).ToList();
        }
      }

      return rtnTopic;
    }

    #region GetUserTopics
    public IQueryable<UserTopic> GetUserTopics(Guid UserId)
    {
      return this.mainContext.UserTopics.Where(ut => ut.UserId == UserId);
    }

    public IQueryable<UserTopic> GetUserTopics(Guid UserId, Guid TopicId)
    {
      return GetUserTopics(UserId).Where(ut => ut.TopicId == TopicId);
    }

    #endregion
    #region GetTopic

    public Topic GetTopic(Guid TopicId)
    {
      return this.mainContext.Topics.Where(t => t.TopicId == TopicId).FirstOrDefault();
    }
    #endregion

    public IEnumerable<TopicVM> GetUsersTopics(Guid UserId)
    {
      return GetUserTopics(UserId).ToList()
        .Join(this.mainContext.Topics, ut => ut.TopicId, t => t.TopicId, (ut, t) => new TopicVM(t, ut))
        .OrderByDescending(tv => tv.UserTopic.Status)
        .ThenBy(tv => tv.Name);
    }

    public IEnumerable<TopicVM> GetTopTopics()
    {
      var startDate = DateTime.UtcNow.AddMonths(-1);

      var topicIds = this.mainContext.Mentorships
        .Where(m => m.CreatedDate >= startDate)
        .GroupBy(m => m.TopicId)
        .Select(m => new { topicId = m.Key, count = m.Count() }).ToList()
        .Join(this.mainContext.UserTopics
          .Where(ut => ut.CreatedDate >= startDate)
          .GroupBy(ut => ut.TopicId)
          .Select(g => new { topicId = g.Key, count = g.Count() }).ToList()
          , m => m.topicId, ut => ut.topicId, (m, ut) => new { topicId = m.topicId, count = m.count + ut.count })
        .OrderByDescending(t => t.count)
        .ToList();

      var rtnTopics = new List<TopicVM>();

      if (topicIds.Count() >= 10)
      {
        foreach (var t in topicIds.Take(10))
        {
          rtnTopics.Add(GetTopicVM(t.topicId));
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

    public IEnumerable<Keyword> GetKeywords()
    {
      IEnumerable<Keyword> rtnList = rtnList = this.mainContext.Keywords;

      if (!this.Actor.Roles.Contains("Admin"))
      {
        rtnList = rtnList.Where(k => k.Active && !k.Restricted);
      }

      return rtnList.OrderBy(k => k.Name).ToList();
    }

    public void SaveKeywords(List<Keyword> Keywords, Guid TopicId)
    {
      if (Keywords != null)
      {
        var newKeywords = Keywords.ToList();
        newKeywords.RemoveAll(k => this.mainContext.Keywords.Select(dbK => dbK.Name.ToLower()).Contains(k.Name.ToLower()));

        var newLinks = Keywords.Join(this.mainContext.Keywords, k => k.Name.ToLower(), dbK => dbK.Name.ToLower(), (k, dbK) => dbK.KeywordId).ToList();
        newLinks.RemoveAll(l => this.mainContext.TopicKeywords.Where(dbTK => dbTK.TopicId == TopicId).Select(dbTK => dbTK.KeywordId).Contains(l));

        var deletedTopicKeywords = this.mainContext.TopicKeywords.Where(tk => tk.TopicId == TopicId).ToList();
        deletedTopicKeywords.RemoveAll(tk => Keywords.Select(dbK => dbK.KeywordId).Contains(tk.KeywordId));

        foreach (var k in newKeywords)
        {
          k.Name = k.Name.ToLower();
          this.mainContext.Keywords.Add(k);
          newLinks.Add(k.KeywordId);
        }

        foreach (var k in newLinks)
        {
          this.mainContext.TopicKeywords.Add(new TopicKeyword()
          {
            TopicId = TopicId,
            KeywordId = k
          });
        }

        foreach (var tk in deletedTopicKeywords)
        {
          this.mainContext.TopicKeywords.Remove(tk);
        }

        this.mainContext.SaveChanges();
      }
    }

    public void SaveLinks(List<Link> Links, Guid TopicId)
    {
      if (Links != null)
      {
        var newLinks = Links.ToList();
        newLinks.RemoveAll(l => this.mainContext.Links.Select(dbL => dbL.Destination.ToLower()).Contains(l.Destination.ToLower()));

        var newTopicLinks = Links.Join(this.mainContext.Links, l => l.Destination.ToLower(), dbL => dbL.Destination.ToLower(), (l, dbL) => dbL.LinkId).ToList();
        newTopicLinks.RemoveAll(l => this.mainContext.TopicLinks.Where(dbTL => dbTL.TopicId == TopicId).Select(dbTL => dbTL.LinkId).Contains(l));

        var deletedTopicLinks = this.mainContext.TopicLinks.Where(tl => tl.TopicId == TopicId).ToList();
        deletedTopicLinks.RemoveAll(tl => Links.Select(dbL => dbL.LinkId).Contains(tl.LinkId));

        foreach (var l in newLinks)
        {
          this.mainContext.Links.Add(l);
          newTopicLinks.Add(l.LinkId);
        }

        foreach (var l in newTopicLinks)
        {
          this.mainContext.TopicLinks.Add(new TopicLink()
          {
            TopicId = TopicId,
            LinkId = l,
            CreatedBy = this.Actor.UserId
          });
        }

        foreach (var tl in deletedTopicLinks)
        {
          this.mainContext.TopicLinks.Remove(tl);
        }

        this.mainContext.SaveChanges();
      }
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