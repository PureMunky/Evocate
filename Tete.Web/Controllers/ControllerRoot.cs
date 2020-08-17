using Microsoft.AspNetCore.Mvc;
using Tete.Models.Authentication;
using Tete.Api.Helpers;

namespace Tete.Api.Controllers
{
  public abstract class ControllerRoot : ControllerBase
  {
    private Contexts.MainContext context;

    public UserVM CurrentUser
    {
      get
      {
        return UserHelper.CurrentUser(HttpContext, this.context);
      }
    }

    public UserVM CurrentAdmin
    {
      get
      {
        var current = UserHelper.CurrentUser(HttpContext, Context);

        if (!current.Roles.Contains("Admin"))
          throw new System.Exception("Not an admin user!");

        return current;
      }
    }

    public Contexts.MainContext Context
    {
      get
      {
        return this.context;
      }
    }

    public ControllerRoot(Contexts.MainContext mainContext)
    {
      this.context = mainContext;
    }

  }
}