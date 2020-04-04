using System;
using System.Collections.Generic;
using System.Linq;
using Tete.Api.Contexts;
using Tete.Models.Localization;
using Tete.Api.Helpers;

namespace Tete.Api.Services.Localization
{

  public class LanguageService
  {
    private MainContext mainContext;

    public LanguageService(MainContext mainContext)
    {
      this.mainContext = mainContext;
    }

    public List<Language> GetLanguages()
    {
      var languages = this.mainContext.Languages.Where(l => l.Active == true).OrderBy(l => l.Name).ToList();
      
      return languages;
    }

    public Language CreateLanguage(string language)
    {
      Language lang = new Language()
      {
        LanguageId = Guid.NewGuid(),
        Name = language,
        Active = true
      };

      return CreateLanguage(lang);
    }

    public Language CreateLanguage(Language language)
    {
      // if (!UserHelper.CurrentUser().Roles.Contains("Admin")) throw new AccessViolationException("Incorrect user permissions.");
      
      this.mainContext.Languages.Add(language);
      this.mainContext.SaveChanges();

      return language;
    }

    public Language Update(Language language)
    {
      // if (!UserHelper.CurrentUser().Roles.Contains("Admin")) throw new AccessViolationException("Incorrect user permissions.");

      this.mainContext.Languages.Update(language);
      this.mainContext.SaveChanges();


      return language;
    }

  }
}