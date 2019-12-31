using System;
using System.Collections.Generic;
using System.Linq;
using Tete.Api.Contexts;
using Tete.Models.Localization;

namespace Tete.Api.Services.Localization
{

  public class LanguageService
  {
    private MainContext mainContext;

    public LanguageService(MainContext mainContext)
    {
      this.mainContext = mainContext;
    }

    public List<UserLanguage> GetUserLanguages(Guid UserId)
    {
      return this.mainContext.UserLanguages.Where(l => l.UserId == UserId).OrderBy(l => l.Priority).ToList();
    }

  }
}