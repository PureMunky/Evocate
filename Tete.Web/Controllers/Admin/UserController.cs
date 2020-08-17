using Microsoft.AspNetCore.Mvc;
using Tete.Web.Models;
using Tete.Api.Controllers;

namespace Tete.Api.AdminControllers
{
  [Route("Admin/[controller]/[action]")]
  [ApiController]
  public class UserController : ControllerRoot
  {

    public UserController(Contexts.MainContext mainContext) : base(mainContext)
    {

    }
  }
}