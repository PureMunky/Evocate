using System;
using System.Linq;
using Tete.Api.Contexts;
using Tete.Models.Authentication;

namespace Tete.Api.Services.Users
{
  public class ProfileService
  {

    private MainContext mainContext;

    public ProfileService(MainContext mainContext)
    {
      this.mainContext = mainContext;
    }

    public UserVM GetUser(Guid UserId)
    {
        return new UserVM(
            this.mainContext.Users.Where(u => u.Id == UserId).FirstOrDefault(),
            new System.Collections.Generic.List<Models.Localization.UserLanguage>(),
            new Models.Users.Profile(UserId));
    }

  }
}