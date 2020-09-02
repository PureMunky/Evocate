using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tete.Models.Localization;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]")]
  [ApiController]
  public class UserLanguagesController : ControllerRoot
  {


    public UserLanguagesController(Contexts.MainContext mainContext) : base(mainContext)
    {
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public ActionResult<List<UserLanguage>> Get(string id)
    {
      LogService.Write("Get user Languages", id, "Api");

      return new Services.Localization.UserLanguageService(Context, CurrentUser).GetUserLanguages(new Guid(id));
    }

  }
}