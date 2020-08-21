using System;
using System.Linq;
using System.Collections.Generic;
using Tete.Api.Contexts;
using Tete.Api.Services.Localization;
using Tete.Models.Relationships;
using Tete.Models.Authentication;
using Tete.Models.Content;
using Tete.Models.Users;

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
      var topic = TopicService.GetTopic(TopicId);
      if (topic != null && (!topic.Elligible || this.Actor.Roles.Contains("Admin")))
      {
        SetUserTopic(UserId, TopicId, TopicStatus.Mentor);
      }
    }

    public MentorshipVM ClaimNextMentorship(Guid UserId, Guid TopicId)
    {
      MentorshipVM rtnMentorship = null;

      if (UserId == this.Actor.UserId || this.Actor.Roles.Contains("Admin"))
      {
        var dbMentorship = OpenMentorships(UserId, TopicId).OrderBy(m => m.CreatedDate).FirstOrDefault();
        var dbUserTopic = TopicService.GetUserTopics(UserId, TopicId).FirstOrDefault();

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

    public IQueryable<Mentorship> OpenMentorships(Guid UserId, Guid TopicId)
    {
      return this.mainContext.Mentorships.Where(m => m.Active && m.TopicId == TopicId && m.MentorUserId == Guid.Empty && m.LearnerUserId != UserId);
    }

    public List<MentorshipVM> GetUserMentorships(Guid UserId)
    {
      var rtnList = new List<MentorshipVM>();

      if (UserId == this.Actor.UserId || this.Actor.Roles.Contains("Admin"))
      {
        var dbMentorships = this.mainContext.Mentorships.Where(m => m.Active && ((m.LearnerUserId == UserId && !m.LearnerClosed) || (m.MentorUserId == UserId && !m.MentorClosed))).ToList();

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

    public MentorshipVM CloseMentorship(Evaluation Evaluation)
    {
      MentorshipVM rtnMentorship = null;
      Evaluation evaluation = null;
      var dbMentorship = this.mainContext.Mentorships.Where(m => m.MentorshipId == Evaluation.MentorshipId).FirstOrDefault();

      if (dbMentorship != null)
      {
        if (dbMentorship.MentorUserId == this.Actor.UserId && !dbMentorship.MentorClosed)
        {
          dbMentorship.MentorClosed = true;
          dbMentorship.MentorClosedDate = DateTime.UtcNow;
          evaluation = new Evaluation(dbMentorship.MentorshipId, dbMentorship.LearnerUserId, EvaluationUserType.Learner);
        }
        else if (dbMentorship.LearnerUserId == this.Actor.UserId && !dbMentorship.LearnerClosed)
        {
          dbMentorship.LearnerClosed = true;
          dbMentorship.LearnerClosedDate = DateTime.UtcNow;
          evaluation = new Evaluation(dbMentorship.MentorshipId, dbMentorship.MentorUserId, EvaluationUserType.Mentor);
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

        if (evaluation != null)
        {
          var dbEvaluation = this.mainContext.Evaluations.Where(e => e.MentorshipId == evaluation.MentorshipId && e.UserId == evaluation.UserId).FirstOrDefault();
          if (dbEvaluation == null)
          {
            evaluation.Comments = Evaluation.Comments;
            evaluation.Rating = Evaluation.Rating;
            this.mainContext.Evaluations.Add(evaluation);
          }
        }

        this.mainContext.Update(dbMentorship);
        this.mainContext.SaveChanges();

        rtnMentorship = GetMentorship(Evaluation.MentorshipId);
      }


      return rtnMentorship;
    }

    public MentorshipVM CancelMentorship(Guid MentorshipId)
    {
      var dbMentorship = this.mainContext.Mentorships.Where(m => m.MentorshipId == MentorshipId).FirstOrDefault();

      if (dbMentorship != null && dbMentorship.MentorUserId == Guid.Empty)
      {
        dbMentorship.LearnerClosed = true;
        dbMentorship.EndDate = DateTime.UtcNow;
        dbMentorship.Active = false;
        this.mainContext.Update(dbMentorship);
        this.mainContext.SaveChanges();
      }

      return GetMentorship(MentorshipId);
    }

    public void RateMentorship(Evaluation evaluation)
    {
      var dbEvaluation = this.mainContext.Evaluations.Where(e => e.MentorshipId == evaluation.MentorshipId && e.UserId == evaluation.UserId).FirstOrDefault();
      var dbMentorship = this.mainContext.Mentorships.Where(m => m.MentorshipId == evaluation.MentorshipId).FirstOrDefault();
      if (
        dbEvaluation == null
        && dbMentorship != null
        && (
          (dbMentorship.LearnerUserId == evaluation.UserId && dbMentorship.MentorUserId == this.Actor.UserId)
          ||
          (dbMentorship.MentorUserId == evaluation.UserId && dbMentorship.LearnerUserId == this.Actor.UserId)
        )
      )
      {

      }
    }

    #endregion

    #region Private Functions

    // TODO: Consider moving SetUserTopic to TopicService.
    private void SetUserTopic(Guid UserId, Guid TopicId, TopicStatus topicStatus)
    {
      if (UserId == this.Actor.UserId || this.Actor.Roles.Contains("Admin"))
      {
        var dbUserTopic = TopicService.GetUserTopics(UserId, TopicId).FirstOrDefault();

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
      var dbTopic = TopicService.GetTopic(mentorship.TopicId);

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