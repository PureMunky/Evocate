using Microsoft.AspNetCore.Mvc;
using Tete.Web.Helpers;

namespace Tete.Web.Controllers
{
  public class HomeController : Controller
  {
    [Route("")]
    public IActionResult Index()
    {
      if (HttpContext.Request.Cookies.Keys.Contains(Constants.SessionTokenName))
      {
        return View();
      }
      else
      {
        return Redirect("/Login");
      }
    }
  }
}