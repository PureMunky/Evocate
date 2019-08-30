using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Authentication
{
  public class Login
  {

    public Guid LoginId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastAccessed { get; set; }

    public Login()
    {
      this.LoginId = Guid.NewGuid();
      this.Created = DateTime.UtcNow;
      this.LastAccessed = DateTime.UtcNow;
    }
  }
}