using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tete.Models.Localization;
using Tete.Api.Helpers;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]")]
  [ApiController]
  public class UserLanguagesController : ControllerBase
  {

    private Api.Services.Logging.LogService logService;
    private Contexts.MainContext context;

    public UserLanguagesController(Contexts.MainContext mainContext)
    {
      this.context = mainContext;
      this.logService = new Services.Logging.LogService(mainContext, Tete.Api.Services.Logging.LogService.LoggingLayer.Api);
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public ActionResult<List<UserLanguage>> Get(string id)
    {
      this.logService.Write("Get user Languages", id, "Api");
      var CurrentUser = UserHelper.CurrentUser(HttpContext, this.context);

      return new Services.Localization.UserLanguageService(this.context, CurrentUser).GetUserLanguages(new Guid(id));
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody] string value)
    {
      //this.service.CreateLanguage(value);
    }

  }
}