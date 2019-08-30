using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Authentication
{
  public class Session
  {

    [Key]
    public Guid SessionId { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public DateTime Created { get; set; }

    public Session()
    {
      this.SessionId = Guid.NewGuid();
      this.Created = DateTime.UtcNow;
    }
  }
}