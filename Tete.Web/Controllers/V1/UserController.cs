using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Tete.Models.Authentication;
using Tete.Api.Helpers;
using Tete.Web.Models;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]/[action]")]
  [ApiController]
  public class UserController : ControllerBase
  {

    private Api.Services.Logging.LogService logService;

    private Contexts.MainContext context;

    public UserController(Contexts.MainContext mainContext)
    {
      this.context = mainContext;
      this.logService = new Services.Logging.LogService(mainContext, Tete.Api.Services.Logging.LogService.LoggingLayer.Api);
    }

    // POST api/values
    [HttpPost]
    public Response<UserVM> Post([FromBody] UserVM value)
    {
      var CurrentUser = UserHelper.CurrentUser(HttpContext, this.context);

      var userService = new Services.Users.UserService(this.context, CurrentUser);
      userService.SaveUser(value);

      var profService = new Services.Users.ProfileService(this.context, CurrentUser);
      profService.SaveProfile(value.Profile);

      var langService = new Services.Localization.UserLanguageService(this.context, CurrentUser);
      langService.SaveUserLanguages(value.UserId, value.Languages);

      return new Response<UserVM>(value);
    }

  }
}