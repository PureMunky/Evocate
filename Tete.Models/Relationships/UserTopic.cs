using System;
using System.ComponentModel.DataAnnotations;
using Tete.Models.Content;

namespace Tete.Models.Relationships
{

  public class UserTopic
  {

    [Key]
    public Guid UserTopicID { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public Authentication.User User { get; set; }

    [Required]
    public Guid TopicId { get; set; }
    public Topic Topic { get; set; }

    [Required]
    public TopicStatus Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public UserTopic()
    {
      this.UserTopicID = Guid.NewGuid();
    }

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