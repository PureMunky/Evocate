using System;
using System.Collections.Generic;
using System.Linq;
using Tete.Api.Contexts;
using Tete.Models.Authentication;
using Tete.Models.Localization;

namespace Tete.Api.Services.Localization
{

  public class UserLanguageService : ServiceBase
  {
    public UserLanguageService(MainContext mainContext, UserVM actor)
    {
      this.mainContext = mainContext;
      this.Actor = actor;
    }

    public List<UserLanguage> GetUserLanguages(Guid UserId)
    {
      var languages = new List<UserLanguage>();

      try
      {
        languages = this.mainContext.UserLanguages.Where(l => l.UserId == UserId).OrderBy(l => l.Priority).ToList();
      }
      catch (Exception)
      {
        languages = new List<UserLanguage>();
      }

      return languages;
    }

    public void SaveUserLanguages(Guid UserId, List<UserLanguage> Languages)
    {
      if (UserId == this.Actor.UserId || this.Actor.Roles.Contains("Admin"))
      {
        var previousLanguages = GetUserLanguages(UserId);
        var newLangIds = new List<Guid>();

        // TODO: Update priority, remove old languages, add new ones.
        // TODO: Validate each userlanguage associates with the passed userId.
        for (int i = 0; i < Languages.Count; i++)
        {
          var found = false;
          var newUl = Languages[i];
          newLangIds.Add(newUl.LanguageId);
          newUl.Priority = i + 1;
          newUl.UserId = UserId;

          foreach (UserLanguage ul in previousLanguages)
          {
            if (ul.LanguageId == newUl.LanguageId)
            {
              ul.Speak = newUl.Speak;
              ul.Read = newUl.Read;
              ul.Priority = newUl.Priority;
              found = true;
            }
          }

          if (!found)
          {
            this.mainContext.Add(newUl);
          }
        }

        foreach (UserLanguage ul in previousLanguages)
        {
          if (!newLangIds.Contains(ul.LanguageId))
          {
            this.mainContext.Remove(ul);
          }
        }

        this.mainContext.SaveChanges();
      }
    }
  }

}