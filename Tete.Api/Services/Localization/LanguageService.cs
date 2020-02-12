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

    public List<LanguageVM> GetLanguages()
    {
      var languages = this.mainContext.Languages.Where(l => l.Active == true).OrderBy(l => l.Name).ToList();
      
      var rtnLanguages = new List<LanguageVM>();
      foreach(Language l in languages) {
        rtnLanguages.Add(new LanguageVM(l));
      }

      return rtnLanguages;
    }

    public Language CreateLanguage(string language)
    {
      Language lang = new Language()
      {
        LanguageId = Guid.NewGuid(),
        Name = language,
        Active = true
      };

      this.mainContext.Languages.Add(lang);
      this.mainContext.SaveChanges();

      return lang;
    }

    public List<UserLanguage> GetUserLanguages(Guid UserId)
    {
      return this.mainContext.UserLanguages.Where(l => l.UserId == UserId).OrderBy(l => l.Priority).ToList();
    }

  }
}