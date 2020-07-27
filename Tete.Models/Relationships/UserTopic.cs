using System;

namespace Tete.Models.Relationships
{

  public class UserTopic
  {

    public Guid UserId { get; set; }

    public Guid TopicId { get; set; }

    public TopicStatus Status { get; set; }

    public DateTime StartDate { get; set; }
  }
}