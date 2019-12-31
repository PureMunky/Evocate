using System;
using System.Collections.Generic;
using Tete.Models.Localization;

namespace Tete.Models.Authentication
{
  public class UserVM
  {
    public string DisplayName { get; set; }

    public string Email { get; set; }

    public string UserName { get; set; }

    public List<UserLanguage> Languages { get; set; }

    public UserVM(User user, List<UserLanguage> languages)
    {
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
    }
  }
}