using System;
using System.Linq;
using Tete.Api.Contexts;
using Tete.Api.Services.Localization;
using Tete.Models.Authentication;
using Tete.Models.Users;

namespace Tete.Api.Services.Users
{
  public class ProfileService : ServiceBase
  {


    public ProfileService(MainContext mainContext, UserVM actor)
    {
      FillData(mainContext, actor);
    }

    public void SaveProfile(Profile profile)
    {
      var prof = this.mainContext.UserProfiles.Where(p => p.ProfileId == profile.ProfileId).FirstOrDefault();

      if (prof is null)
      {
        this.mainContext.UserProfiles.Add(profile);
      }
      else
      {
        if (
          prof.UserId == this.Actor.UserId
          || this.Actor.Roles.Contains("Admin")
          )
        {
          prof.About = profile.About;
          prof.PrivateAbout = profile.PrivateAbout;
          this.mainContext.UserProfiles.Update(prof);
        }
      }
      this.mainContext.SaveChanges();
    }

    private void FillData(MainContext mainContext, UserVM actor)
    {
      this.mainContext = mainContext;
      this.Actor = actor;
    }
  }
}