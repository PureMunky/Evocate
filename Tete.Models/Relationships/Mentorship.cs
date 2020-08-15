using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Relationships
{

  public class Mentorship
  {

    [Required]
    public Guid MentorshipId { get; set; }

    [Required]
    public Guid TopicId { get; set; }

    [Required]
    public bool Active { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    #region Learner Details

    [Required]
    public Guid LearnerUserId { get; set; }

    public string LearnerContact { get; set; }

    public bool LearnerClosed { get; set; }

    public DateTime LearnerClosedDate { get; set; }

    [Range(1, 5)]
    public int LearnerRating { get; set; }

    public string LearnerClosingComments { get; set; }

    #endregion

    #region Mentor Details

    // A mentorship without a mentor user id is a request for a mentor.
    public Guid MentorUserId { get; set; }

    public string MentorContact { get; set; }

    public bool MentorClosed { get; set; }

    public DateTime MentorClosedDate { get; set; }

    [Range(1, 5)]
    public int MentorRating { get; set; }

    public string MentorClosingComments { get; set; }

    #endregion

    public Mentorship(Guid LearnerUserId, Guid TopicId)
    {
      this.MentorshipId = Guid.NewGuid();
      this.LearnerUserId = LearnerUserId;
      this.MentorUserId = Guid.Empty;
      this.TopicId = TopicId;
      this.Active = true;
      this.CreatedDate = DateTime.UtcNow;
      this.LearnerClosed = false;
      this.MentorClosed = false;
    }
  }
}