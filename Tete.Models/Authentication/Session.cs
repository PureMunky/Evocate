using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Authentication
{
  public class Session
  {

    [Key]
    public string Token { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public DateTime Created { get; set; }

    public DateTime LastUsed { get; set; }

    public Session()
    {
      this.Created = DateTime.UtcNow;
      this.LastUsed = DateTime.UtcNow;
    }
  }
}