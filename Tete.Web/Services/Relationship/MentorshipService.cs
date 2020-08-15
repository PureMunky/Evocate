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

    #region Private Variables

    private UserLanguageService userLanguageService;
    private Logging.LogService logService;

    #endregion

    #region Public Functions

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
        var dbMentorship = this.mainContext.Mentorships.Where(m => m.Active == true && m.MentorUserId == Guid.Empty && m.LearnerUserId != UserId).OrderBy(m => m.CreatedDate).FirstOrDefault();
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

    public MentorshipVM SetContactDetails(ContactUpdate contactDetails)
    {
      if (contactDetails.UserId == this.Actor.UserId || this.Actor.Roles.Contains("Admin"))
      {
        var dbMentorship = this.mainContext.Mentorships.Where(m => m.MentorshipId == contactDetails.MentorshipId).FirstOrDefault();

        if (
          dbMentorship != null
          && (
            dbMentorship.LearnerUserId == contactDetails.UserId
            || dbMentorship.MentorUserId == contactDetails.UserId
          )
        )
        {
          if (dbMentorship.LearnerUserId == contactDetails.UserId)
          {
            dbMentorship.LearnerContact = contactDetails.ContactDetails;
          }
          else if (dbMentorship.MentorUserId == contactDetails.UserId)
          {
            dbMentorship.MentorContact = contactDetails.ContactDetails;
          }

          this.mainContext.Mentorships.Update(dbMentorship);
          this.mainContext.SaveChanges();
        }
      }

      return GetMentorship(contactDetails.MentorshipId);
    }

    public MentorshipVM CloseMentorship(MentorshipVM mentorship)
    {
      MentorshipVM rtnMentorship = null;
      var dbMentorship = this.mainContext.Mentorships.Where(m => m.MentorshipId == mentorship.MentorshipId).FirstOrDefault();

      if (dbMentorship != null)
      {
        if (dbMentorship.MentorUserId == this.Actor.UserId && !dbMentorship.MentorClosed)
        {
          dbMentorship.MentorClosed = true;
          dbMentorship.MentorClosedDate = DateTime.UtcNow;
          dbMentorship.MentorClosingComments = mentorship.MentorClosingComments;
          dbMentorship.LearnerRating = mentorship.LearnerRating;
        }
        else if (dbMentorship.LearnerUserId == this.Actor.UserId && !dbMentorship.LearnerClosed)
        {
          dbMentorship.LearnerClosed = true;
          dbMentorship.LearnerClosedDate = DateTime.UtcNow;
          dbMentorship.LearnerClosingComments = mentorship.LearnerClosingComments;
          dbMentorship.MentorRating = mentorship.MentorRating;
        }
        else if (this.Actor.Roles.Contains("Admin"))
        {
          dbMentorship.Active = false;
          dbMentorship.EndDate = DateTime.UtcNow;
        }

        if (dbMentorship.LearnerClosed && dbMentorship.MentorClosed && dbMentorship.Active)
        {
          dbMentorship.Active = false;
          dbMentorship.EndDate = DateTime.UtcNow;
        }

        this.mainContext.Update(dbMentorship);
        this.mainContext.SaveChanges();

        rtnMentorship = GetMentorship(mentorship.MentorshipId);
      }


      return rtnMentorship;
    }
    #endregion

    #region Private Functions

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

    private MentorshipVM GetMentorshipVM(Mentorship mentorship)
    {
      var dbTopic = this.mainContext.Topics.Where(t => t.TopicId == mentorship.TopicId).FirstOrDefault();

      var rtnMentorship = new MentorshipVM(mentorship, new TopicVM(dbTopic));

      var dbLearner = this.mainContext.Users.Where(u => u.Id == mentorship.LearnerUserId).FirstOrDefault();
      if (dbLearner != null)
      {
        rtnMentorship.Learner = new UserVM(dbLearner);
      }

      if (rtnMentorship.HasMentor)
      {
        var dbMentor = this.mainContext.Users.Where(u => u.Id == mentorship.MentorUserId).FirstOrDefault();

        if (dbMentor != null)
        {
          rtnMentorship.Mentor = new UserVM(dbMentor);
        }
        else
        {
          rtnMentorship.MentorshipId = Guid.Empty;
          rtnMentorship.HasMentor = false;
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

    #endregion

  }

}