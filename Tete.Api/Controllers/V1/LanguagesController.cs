using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Tete.Models.Localization;
using Tete.Api.Helpers;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]")]
  [ApiController]
  public class LanguagesController : ControllerBase
  {

    private Api.Services.Logging.LogService logService;

    private Contexts.MainContext context;

    public LanguagesController(Contexts.MainContext mainContext)
    {
      this.context = mainContext;
      this.logService = new Services.Logging.LogService(mainContext, "API");
    }
    // GET api/values
    [HttpGet]
    public IEnumerable<Language> Get()
    {
      var service  = new Services.Localization.LanguageService(this.context, UserHelper.CurrentUser(HttpContext, this.context));
      
      return service.GetLanguages();
    }

    // POST api/values
    [HttpPost]
    public ActionResult<Language> Post([FromBody] string value)
    {
      var service  = new Services.Localization.LanguageService(this.context, UserHelper.CurrentUser(HttpContext, this.context)); 

      return service.CreateLanguage(value);
    }

    [HttpPut]
    public ActionResult<Language> Update([FromBody] Language language)
    {
      var service  = new Services.Localization.LanguageService(this.context, UserHelper.CurrentUser(HttpContext, this.context));
      
      return service.Update(language);
    }

  }
}