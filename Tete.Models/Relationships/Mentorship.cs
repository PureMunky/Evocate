using System;

namespace Tete.Models.Relationships
{

  public class Mentorship
  {

    public Guid MentorshipId { get; set; }

    public Guid LearnerUserId { get; set; }

    public Guid MentorUserId { get; set; }

    public Guid TopicId { get; set; }

    public bool Active { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
  }
}