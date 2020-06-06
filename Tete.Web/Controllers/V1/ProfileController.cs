using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Tete.Models.Users;
using Tete.Api.Helpers;
using Tete.Web.Models;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]/[action]")]
  [ApiController]
  public class ProfileController : ControllerBase
  {

    private Api.Services.Logging.LogService logService;

    private Contexts.MainContext context;

    public ProfileController(Contexts.MainContext mainContext)
    {
      this.context = mainContext;
      this.logService = new Services.Logging.LogService(mainContext, "API");
    }

    // POST api/values
    [HttpPost]
    public Response<Profile> Post([FromBody] Profile value)
    {
      var service = new Services.Users.ProfileService(this.context, UserHelper.CurrentUser(HttpContext, this.context));
      service.SaveProfile(value);

      return new Response<Profile>(value);
    }

    // [HttpPut]
    // public Response<Language> Update([FromBody] Language language)
    // {
    //   var service = new Services.Localization.LanguageService(this.context, UserHelper.CurrentUser(HttpContext, this.context));

    //   return new Response<Language>(service.Update(language));
    // }

  }
}