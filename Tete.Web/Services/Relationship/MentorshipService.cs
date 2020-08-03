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

    private void SetUserTopic(Guid UserId, Guid TopicId, TopicStatus topicStatus)
    {
      if (UserId == this.Actor.UserId || this.Actor.Roles.Contains("Admin"))
      {
        var dbUserTopic = this.mainContext.UserTopics.Where(t => t.UserId == UserId && t.TopicId == t.TopicId).FirstOrDefault();

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
        var dbMentorships = this.mainContext.Mentorships.Where(m => m.LearnerUserId == UserId);

        foreach (Mentorship m in dbMentorships)
        {
          var dbTopic = this.mainContext.Topics.Where(t => t.TopicId == m.TopicId).FirstOrDefault();

          var mentorship = new MentorshipVM(m, new TopicVM(dbTopic));
          if (mentorship.HasMentor)
          {
            var dbMentor = this.mainContext.Users.Where(u => u.Id == m.MentorUserId).FirstOrDefault();

            if (dbMentor != null)
            {
              mentorship.Mentor = new UserVM(dbMentor);
            }
            else
            {
              mentorship.MentorshipId = Guid.Empty;
              mentorship.HasMentor = false;
            }
          }

          rtnList.Add(mentorship);
        }

      }

      return rtnList;
    }
    // public Mentorship GetMentorship(Guid MentorshipId)
    // {
    //   // var dbTopic = this.mainContext.Topics.Where(t => t.TopicId == topicId).FirstOrDefault();
    //   Mentorship rtnMentorship = new Mentorship();

    //   if (dbTopic != null)
    //   {
    //     rtnTopic = new TopicVM(dbTopic);
    //   }

    //   return rtnTopic;
    // }

    private void FillData(MainContext mainContext, UserVM actor)
    {
      this.mainContext = mainContext;
      this.Actor = actor;
      this.userLanguageService = new UserLanguageService(mainContext, actor);
      this.logService = new Logging.LogService(mainContext, Logging.LogService.LoggingLayer.Api);
    }
  }
}