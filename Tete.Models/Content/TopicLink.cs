using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Content
{
  public class TopicLink
  {
    public Guid TopicLinkId { get; set; }

    [Required]
    public Guid TopicId { get; set; }
    public Topic Topic { get; set; }

    [Required]
    public Guid LinkId { get; set; }
    public Link Link { get; set; }

    public bool Active { get; set; }

    public DateTime Created { get; set; }

    public DateTime CreatedBy { get; set; }

    public TopicLink()
    {
      this.TopicLinkId = Guid.NewGuid();
      this.Active = true;
      this.Created = DateTime.UtcNow;
    }

  }
}