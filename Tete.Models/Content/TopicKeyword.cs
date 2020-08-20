using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Content
{
  public class TopicKeyword
  {
    public Guid TopicKeywordId { get; set; }

    [Required]
    public Guid TopicId { get; set; }
    public Topic Topic { get; set; }

    public Guid KeywordId { get; set; }
    public Keyword Keyword { get; set; }

    public TopicKeyword()
    {
      this.TopicKeywordId = Guid.NewGuid();
    }
  }
}