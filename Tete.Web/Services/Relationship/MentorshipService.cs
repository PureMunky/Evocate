using System;
using System.Linq;
using System.Collections.Generic;
using Tete.Api.Contexts;
using Tete.Api.Services.Localization;
using Tete.Models.Relationships;
using Tete.Models.Authentication;
using Tete.Models.Content;

namespace Tete.Api.Services.Relationships
{
  public class MentorshipService : ServiceBase
  {

    private UserLanguageService userLanguageService;
    private Logging.LogService logService;

    public MentorshipService(MainContext mainContext, UserVM actor)
    {
      FillData(mainContext, actor);
    }

    public void RegisterLearner(Guid UserId, Guid TopicId)
    {
      if (UserId == this.Actor.UserId || this.Actor.Roles.Contains("Admin"))
      {
        var dbMentorship = this.mainContext.Mentorships.Where(m => m.LearnerUserId == UserId && m.TopicId == TopicId && m.Active).FirstOrDefault();

        if (dbMentorship == null)
        {
          var newMentorship = new Mentorship(UserId, TopicId);
          this.mainContext.Mentorships.Add(newMentorship);
        }

        SetUserTopic(UserId, TopicId, TopicStatus.Novice);

        this.mainContext.SaveChanges();
      }
    }

    public void RegisterMentor(Guid UserId, Guid TopicId)
    {
      SetUserTopic(UserId, TopicId, TopicStatus.Mentor);
    }

    public MentorshipVM ClaimNextMentorship(Guid UserId, Guid TopicId)
    {
      MentorshipVM rtnMentorship = null;

      if (UserId == this.Actor.UserId || this.Actor.Roles.Contains("Admin"))
      {
        var dbMentorship = this.mainContext.Mentorships.Where(m => m.Active == true && m.MentorUserId == Guid.Empty).OrderBy(m => m.CreatedDate).FirstOrDefault();
        var dbUserTopic = this.mainContext.UserTopics.Where(ut => ut.UserId == UserId).FirstOrDefault();

        if (dbMentorship != null && dbUserTopic != null && dbUserTopic.Status == TopicStatus.Mentor)
        {
          dbMentorship.MentorUserId = UserId;
          dbMentorship.StartDate = DateTime.UtcNow;
          this.mainContext.Update(dbMentorship);
          this.mainContext.SaveChanges();

          rtnMentorship = GetMentorship(dbMentorship.MentorshipId);
        }

      }

      return rtnMentorship;
    }

    private void SetUserTopic(Guid UserId, Guid TopicId, TopicStatus topicStatus)
    {
      if (UserId == this.Actor.UserId || this.Actor.Roles.Contains("Admin"))
      {
        var dbUserTopic = this.mainContext.UserTopics.Where(t => t.UserId == UserId && t.TopicId == TopicId).FirstOrDefault();

        if (dbUserTopic == null)
        {
          var newUserTopic = new UserTopic(UserId, TopicId, topicStatus);
          this.mainContext.UserTopics.Add(newUserTopic);
        }

        this.mainContext.SaveChanges();
      }
    }

    public List<MentorshipVM> GetUserMentorships(Guid UserId)
    {
      var rtnList = new List<MentorshipVM>();

      if (UserId == this.Actor.UserId || this.Actor.Roles.Contains("Admin"))
      {
        var dbMentorships = this.mainContext.Mentorships.Where(m => m.LearnerUserId == UserId || m.MentorUserId == UserId);

        foreach (Mentorship m in dbMentorships)
        {
          rtnList.Add(GetMentorshipVM(m));
        }

      }

      return rtnList;
    }
    public MentorshipVM GetMentorship(Guid MentorshipId)
    {
      var dbMentorship = this.mainContext.Mentorships.Where(m => m.MentorshipId == MentorshipId).FirstOrDefault();
      MentorshipVM rtnMentorship = null;

      if (dbMentorship != null)
      {
        if (this.Actor.Roles.Contains("Admin") || dbMentorship.MentorUserId == this.Actor.UserId || dbMentorship.LearnerUserId == this.Actor.UserId)
        {
          rtnMentorship = GetMentorshipVM(dbMentorship);
        }
      }

      return rtnMentorship;
    }

    private MentorshipVM GetMentorshipVM(Mentorship mentorship)
    {
      var dbTopic = this.mainContext.Topics.Where(t => t.TopicId == mentorship.TopicId).FirstOrDefault();

      var rtnMentorship = new MentorshipVM(mentorship, new TopicVM(dbTopic));
      if (rtnMentorship.HasMentor)
      {
        var dbMentor = this.mainContext.Users.Where(u => u.Id == mentorship.MentorUserId).FirstOrDefault();
        var dbLearner = this.mainContext.Users.Where(u => u.Id == mentorship.LearnerUserId).FirstOrDefault();

        if (dbMentor != null)
        {
          rtnMentorship.Mentor = new UserVM(dbMentor);
        }
        else
        {
          rtnMentorship.MentorshipId = Guid.Empty;
          rtnMentorship.HasMentor = false;
        }

        if (dbLearner != null)
        {
          rtnMentorship.Learner = new UserVM(dbLearner);
        }
      }

      return rtnMentorship;
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