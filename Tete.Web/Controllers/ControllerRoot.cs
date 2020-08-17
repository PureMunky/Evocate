using Microsoft.AspNetCore.Mvc;
using Tete.Models.Authentication;
using Tete.Api.Helpers;

namespace Tete.Api.Controllers
{
  public class ControllerRoot : ControllerBase
  {
    private Contexts.MainContext context;

    public UserVM CurrentUser
    {
      get
      {
        return UserHelper.CurrentUser(HttpContext, this.context);
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