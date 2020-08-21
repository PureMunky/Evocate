using System;

namespace Tete.Models.Relationships
{
  public class ContactUpdate
  {
    public Guid MentorshipId { get; set; }
    public Guid UserId { get; set; }
    public string ContactDetails { get; set; }
  }
}