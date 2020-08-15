using System;
using Tete.Models.Content;
using Tete.Models.Authentication;

namespace Tete.Models.Relationships
{

  public class MentorshipVM : Mentorship
  {

    public TopicVM Topic { get; set; }
    public UserVM Mentor { get; set; }
    public UserVM Learner { get; set; }
    public bool HasMentor { get; set; }

    public MentorshipVM(Guid LearnerUserId, Guid TopicId, TopicVM Topic) : base(LearnerUserId, TopicId)
    {
      this.Topic = Topic;
    }

    public MentorshipVM(Mentorship mentorship, TopicVM Topic) : base(mentorship.LearnerUserId, mentorship.TopicId)
    {
      this.MentorshipId = mentorship.MentorshipId;
      this.LearnerUserId = mentorship.LearnerUserId;
      this.LearnerContact = mentorship.LearnerContact;
      this.MentorUserId = mentorship.MentorUserId;
      this.MentorContact = mentorship.MentorContact;
      this.TopicId = mentorship.TopicId;
      this.Active = mentorship.Active;
      this.CreatedDate = mentorship.CreatedDate;
      this.StartDate = mentorship.StartDate;
      this.EndDate = mentorship.EndDate;
      this.LearnerClosed = mentorship.LearnerClosed;
      this.LearnerClosedDate = mentorship.LearnerClosedDate;
      this.MentorClosed = mentorship.MentorClosed;
      this.MentorClosedDate = mentorship.MentorClosedDate;
      this.Topic = Topic;
      this.HasMentor = (mentorship.MentorUserId != Guid.Empty);
    }
  }
}