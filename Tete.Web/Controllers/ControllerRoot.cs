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
        var current = UserHelper.CurrentUser(HttpContext, Context);

        if (current == null)
        {
          throw new NotLoggedInException("Not logged in.");
        }

        return current;
      }
    }

    public UserVM CurrentAdmin
    {
      get
      {
        var current = UserHelper.CurrentUser(HttpContext, Context);

        if (!current.Roles.Contains("Admin"))
        {
          throw new InsufficientPriviledgesException("Not an admin user!");
        }

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

    public Tete.Api.Services.Logging.LogService LogService
    {
      get
      {
        return new Tete.Api.Services.Logging.LogService(Context, Tete.Api.Services.Logging.LogService.LoggingLayer.Web, CurrentUser);
      }
    }

    public ControllerRoot(Contexts.MainContext mainContext)
    {
      this.context = mainContext;
    }

  }
}