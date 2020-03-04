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
  public class LanguagesController : ControllerBase
  {

    private Api.Services.Logging.LogService logService;
    private Api.Services.Localization.LanguageService service;

    public LanguagesController(Contexts.MainContext mainContext)
    {
      this.service = new Services.Localization.LanguageService(mainContext);
    }
    // GET api/values
    [HttpGet]
    public IEnumerable<LanguageVM> Get()
    {
      return this.service.GetLanguages();
    }

    // POST api/values
    [HttpPost]
    public ActionResult<Language> Post([FromBody] string value)
    {
      return this.service.CreateLanguage(value);
    }

  }
}