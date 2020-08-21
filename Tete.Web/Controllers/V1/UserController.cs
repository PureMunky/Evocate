using Microsoft.AspNetCore.Mvc;
using Tete.Models.Authentication;
using Tete.Api.Helpers;
using Tete.Web.Models;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]/[action]")]
  [ApiController]
  public class UserController : ControllerRoot
  {

    private Api.Services.Logging.LogService logService;

    public UserController(Contexts.MainContext mainContext) : base(mainContext)
    {
      this.logService = new Services.Logging.LogService(mainContext, Tete.Api.Services.Logging.LogService.LoggingLayer.Api);
    }

    // POST api/values
    [HttpPost]
    public Response<UserVM> Post([FromBody] UserVM value)
    {
      var userService = new Services.Users.UserService(Context, CurrentUser);
      userService.SaveUser(value);

      var profService = new Services.Users.ProfileService(Context, CurrentUser);
      profService.SaveProfile(value.Profile);

      var langService = new Services.Localization.UserLanguageService(Context, CurrentUser);
      langService.SaveUserLanguages(value.UserId, value.Languages);

      return new Response<UserVM>(value);
    }

    [HttpGet]
    public Response<UserVM> Search(string searchText)
    {
      var service = new Services.Users.UserService(Context, CurrentAdmin);

      return new Response<UserVM>(service.Search(searchText));
    }
  }
}