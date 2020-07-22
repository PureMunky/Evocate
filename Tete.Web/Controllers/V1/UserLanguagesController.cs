using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tete.Models.Localization;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]")]
  [ApiController]
  public class UserLanguagesController : ControllerBase
  {

    private Api.Services.Logging.LogService logService;
    private Api.Services.Localization.UserLanguageService service;

    public UserLanguagesController(Contexts.MainContext mainContext)
    {
      this.service = new Services.Localization.UserLanguageService(mainContext);
      this.logService = new Services.Logging.LogService(mainContext, Tete.Api.Services.Logging.LogService.LoggingLayer.Api);
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public ActionResult<List<UserLanguage>> Get(string id)
    {
      this.logService.Write("Get user Languages", id, "Api");
      return this.service.GetUserLanguages(new Guid(id));
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody] string value)
    {
      //this.service.CreateLanguage(value);
    }

  }
}