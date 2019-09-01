using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Authentication
{

  /// <summary>
  /// The simplest user object.
  /// </summary>
  public class User
  {

    public string DisplayName { get; set; }

    public Guid Id { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public byte[] Salt { get; set; }

    [Required]
    public string UserName { get; set; }

    public User()
    {
      this.Id = Guid.NewGuid();
    }

  }
}