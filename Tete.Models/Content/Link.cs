using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Content
{
  public class Link
  {
    public Guid LinkId { get; set; }

    public string Name { get; set; }


    [Required]
    public string Destination { get; set; }

    public bool Active { get; set; }

    public bool Reviewed { get; set; }

    public DateTime Created { get; set; }

    public Guid CreatedBy { get; set; }

    public Link()
    {
      this.LinkId = Guid.NewGuid();
      this.Active = true;
      this.Reviewed = false;
    }
  }
}