using System;
using System.Collections.Generic;
using Tete.Models.Localization;
using Tete.Models.Users;

namespace Tete.Models.Authentication
{

  /// <summary>
  /// class: UserVM
  /// Used to as the public facing view model for a user.
  /// This includes their bio and any other information
  /// that could be displayed for a person.
  /// </summary>
  public class UserVM
  {
    public string DisplayName { get; set; }

    public string Email { get; set; }

    public string UserName { get; set; }

    public List<UserLanguage> Languages { get; set; }

    public Profile Profile { get; set; }

    public List<string> Roles { get; set; }

    public UserVM(User user, List<UserLanguage> languages, Profile profile, List<AccessRole> roles)
    {
      this.DisplayName = "";
      this.Email = "";
      this.UserName = "";
      this.Languages = new List<UserLanguage>();
      this.Profile = new Profile(user.Id);
      this.Roles = new List<string>();

      if (user != null)
      {
        this.DisplayName = user.DisplayName;
        this.Email = user.Email;
        this.UserName = user.UserName;
      }

      if (languages != null)
      {
        this.Languages = languages;
      }

      if (profile != null)
      {
        this.Profile = profile;
      }

      if (roles != null)
      {
        foreach (AccessRole r in roles)
        {
          this.Roles.Add(r.Name);
        }
      }
    }
  }
}