using System;
using System.Collections.Generic;
using System.Linq;
using Tete.Api.Contexts;
using Tete.Models.Localization;
using Tete.Models.Authentication;

namespace Tete.Api.Services.Localization
{

  public class LanguageService : ServiceBase
  {

    public LanguageService(MainContext mainContext, UserVM actor)
    {
      this.mainContext = mainContext;
      this.Actor = actor;
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
      if (!this.Actor.Roles.Contains("Admin")) throw new AccessViolationException("Incorrect user permissions.");
      
      this.mainContext.Languages.Add(language);
      this.mainContext.SaveChanges();

      return language;
    }

    public Language Update(Language language)
    {
      if (!this.Actor.Roles.Contains("Admin")) throw new AccessViolationException("Incorrect user permissions.");

      this.mainContext.Languages.Update(language);
      this.mainContext.SaveChanges();


      return language;
    }

  }
}