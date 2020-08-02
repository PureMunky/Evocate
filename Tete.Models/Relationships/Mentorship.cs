using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Relationships
{

  public class Mentorship
  {

    [Required]
    public Guid MentorshipId { get; set; }

    [Required]
    public Guid LearnerUserId { get; set; }

    // A mentorship without a mentor user id is a request for a mentor.
    public Guid MentorUserId { get; set; }

    [Required]
    public Guid TopicId { get; set; }

    [Required]
    public bool Active { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Mentorship(Guid LearnerUserId, Guid TopicId)
    {
      this.MentorshipId = Guid.NewGuid();
      this.LearnerUserId = LearnerUserId;
      this.MentorUserId = Guid.Empty;
      this.TopicId = TopicId;
      this.Active = true;
      this.CreatedDate = DateTime.UtcNow;

    }
  }
}