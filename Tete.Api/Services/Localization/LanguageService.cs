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
      this.mainContext.Languages.Add(language);
      this.mainContext.SaveChanges();

      return language;
    }

  }
}