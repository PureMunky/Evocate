using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Relationships
{

  public class UserTopic
  {

    [Key]
    public Guid UserTopicID { get; set; }

    [Required]
    public Guid UserId { get; set; }


    [Required]
    public Guid TopicId { get; set; }

    [Required]
    public TopicStatus Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public UserTopic(Guid UserId, Guid TopicId, TopicStatus Status)
    {
      this.UserTopicID = Guid.NewGuid();
      this.UserId = UserId;
      this.TopicId = TopicId;
      this.Status = Status;
      this.CreatedDate = DateTime.UtcNow;
    }
  }

}