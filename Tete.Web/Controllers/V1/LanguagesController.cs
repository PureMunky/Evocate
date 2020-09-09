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
  public class LanguagesController : ControllerRoot
  {


    public LanguagesController(Contexts.MainContext mainContext) : base(mainContext)
    {
    }
    // GET api/values
    [HttpGet]
    public Response<Language> Get()
    {
      var service = new Services.Localization.LanguageService(Context, CurrentUser);

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
      var service = new Services.Localization.LanguageService(Context, CurrentUser);

      LogService.Write("Create Language", string.Format("Name:{0};ID:{1}", value.Name, value.LanguageId));

      return new Response<Language>(service.CreateLanguage(value));
    }

    [HttpPut]
    public Response<Language> Update([FromBody] Language language)
    {
      var service = new Services.Localization.LanguageService(Context, CurrentUser);

      LogService.Write("Update Language", string.Format("Name:{0};ID:{1}", language.Name, language.LanguageId));

      return new Response<Language>(service.Update(language));
    }

  }
}