using System;
using System.Collections.Generic;
using System.Linq;
using Tete.Api.Contexts;
using Tete.Models.Localization;

namespace Tete.Api.Services.Localization
{

  public class UserLanguageService
  {
    private MainContext mainContext;

    public UserLanguageService(MainContext mainContext)
    {
      this.mainContext = mainContext;
    }

    public List<UserLanguage> GetUserLanguages(Guid UserId)
    {
      return this.mainContext.UserLanguages.Where(l => l.UserId == UserId).OrderBy(l => l.Priority).ToList();
    }

  }
}