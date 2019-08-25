using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tete.Web.Helpers;

namespace Tete.Web.Filters
{
  public class Authorized : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      if (!context.HttpContext.Request.Cookies.Keys.Contains(Constants.SessionTokenName))
      {
        context.Result = new RedirectResult("/Login"); ;
      }
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
      // Do something after the action executes.
    }
  }
}