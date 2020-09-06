using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Configuration;
using Tete.Models.Authentication;
using Tete.Web.Helpers;
using Tete.Web.Filters;

namespace Tete.Web.Controllers
{
  public class ErrorController : Controller
  {

    [HttpGet("/error")]
    [Authorized]
    public IActionResult Index()
    {
      return Redirect("/Login");
    }
  }
}