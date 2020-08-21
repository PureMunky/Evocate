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

    private Api.Services.Logging.LogService logService;

    public UserLanguagesController(Contexts.MainContext mainContext) : base(mainContext)
    {
      this.logService = new Services.Logging.LogService(mainContext, Tete.Api.Services.Logging.LogService.LoggingLayer.Api);
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public ActionResult<List<UserLanguage>> Get(string id)
    {
      this.logService.Write("Get user Languages", id, "Api");

      return new Services.Localization.UserLanguageService(Context, CurrentUser).GetUserLanguages(new Guid(id));
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody] string value)
    {
      //this.service.CreateLanguage(value);
    }

  }
}