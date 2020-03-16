using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Users
{
  public class Profile
  {
    public Guid ProfileId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    public string About { get; set; }

    public string PrivateAbout { get; set; }

    public Profile(Guid UserId)
    {
      this.ProfileId = Guid.NewGuid();
      this.UserId = UserId;
      this.About = "";
      this.PrivateAbout = "";
    }

  }
}