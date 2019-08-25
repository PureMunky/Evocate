using Microsoft.AspNetCore.Mvc;
using Tete.Web.Filters;

namespace Tete.Web.Controllers
{
  public class HomeController : Controller
  {
    [Route("")]
    [Authorized]
    public IActionResult Index()
    {
      return View();
    }
  }
}