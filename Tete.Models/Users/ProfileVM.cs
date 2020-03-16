using System;

namespace Tete.Models.Users
{
  public class ProfileVM
  {
    public string About { get; set; }
    public string PrivateAbout { get; set; }

    public ProfileVM()
    {
      this.About = "";
      this.PrivateAbout = "";
    }

    public ProfileVM(Profile profile)
    {
      this.About = profile.About;
      this.PrivateAbout = profile.PrivateAbout;
    }
  }
}