using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Tete.Models.Localization;
using Tete.Api.Helpers;
using Tete.Web.Models;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]/[action]")]
  [ApiController]
  public class LanguagesController : ControllerBase
  {

    private Api.Services.Logging.LogService logService;

    private Contexts.MainContext context;

    public LanguagesController(Contexts.MainContext mainContext)
    {
      this.context = mainContext;
      this.logService = new Services.Logging.LogService(mainContext, Tete.Api.Services.Logging.LogService.LoggingLayer.Api);
    }
    // GET api/values
    [HttpGet]
    public Response<Language> Get()
    {
      var service = new Services.Localization.LanguageService(this.context, UserHelper.CurrentUser(HttpContext, this.context));

      return new Response<Language>(service.GetLanguages());
    }

    [HttpGet]
    public Response<Language> New()
    {
      return new Response<Language>(new Language());
    }

    // POST api/values
    [HttpPost]
    public Response<Language> Post([FromBody] Language value)
    {
      var service = new Services.Localization.LanguageService(this.context, UserHelper.CurrentUser(HttpContext, this.context));

      return new Response<Language>(service.CreateLanguage(value));
    }

    [HttpPut]
    public Response<Language> Update([FromBody] Language language)
    {
      var service = new Services.Localization.LanguageService(this.context, UserHelper.CurrentUser(HttpContext, this.context));

      return new Response<Language>(service.Update(language));
    }

  }
}