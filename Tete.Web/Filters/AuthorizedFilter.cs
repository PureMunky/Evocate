using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tete.Web.Helpers;
using Tete.Models.Authentication;

namespace Tete.Web.Filters
{
  public class Authorized : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext context)
    {

    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
      if (context.Exception is InsufficientPriviledgesException insufficientPriviledgesException)
      {
        context.Result = new ObjectResult(insufficientPriviledgesException.Message)
        {
          StatusCode = 403
        };
        context.ExceptionHandled = true;
      }
      else if (context.Exception is NotLoggedInException notLoggedInException)
      {
        context.Result = new ObjectResult(notLoggedInException.Message)
        {
          StatusCode = 401
        };
        context.ExceptionHandled = true;
      }
      else
      {
        context.Result = new ObjectResult("Error")
        {
          StatusCode = 500
        };
        context.ExceptionHandled = true;
      }
    }
  }
}