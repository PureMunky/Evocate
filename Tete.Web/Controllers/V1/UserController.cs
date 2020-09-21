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


    public UserController(Contexts.MainContext mainContext) : base(mainContext)
    {
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

      LogService.Write("Saved profile", value.UserId.ToString());
      return new Response<UserVM>(value);
    }

    [HttpGet]
    public Response<UserVM> Search(string searchText)
    {
      var service = new Services.Users.UserService(Context, CurrentAdmin);

      return new Response<UserVM>(service.Search(searchText));
    }

    [HttpPost]
    public Response<string> GrantRole([FromBody] RoleUpdate roleUpdate)
    {
      var service = new Services.Users.UserService(Context, CurrentAdmin);
      var success = service.GrantRole(roleUpdate.UserId, roleUpdate.Name);
      return new Response<string>(success ? "Successful" : "Failed", !success);
    }

    [HttpPost]
    public Response<string> RemoveRole([FromBody] RoleUpdate roleUpdate)
    {
      var service = new Services.Users.UserService(Context, CurrentAdmin);
      var success = service.RemoveRole(roleUpdate.UserId, roleUpdate.Name);
      return new Response<string>(success ? "Successful" : "Failed", !success);
    }

    [HttpPost]
    public Response<string> Block([FromBody] UserBlockVM block)
    {
      var service = new Services.Users.UserService(Context, CurrentAdmin);
      var success = service.Block(block);

      return new Response<string>(success ? "Successful" : "Failed", !success);
    }

  }
}