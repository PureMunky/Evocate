using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Content
{
  public class Keyword
  {
    public Guid KeywordId { get; set; }

    public string Name { get; set; }

    public bool Restricted { get; set; }

    public bool Active { get; set; }

    public Keyword()
    {
      this.KeywordId = Guid.NewGuid();
      this.Name = "";
      this.Restricted = false;
      this.Active = true;
    }
  }
}