using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Tete.Models.Content;
using Tete.Models.Relationships;
using Tete.Web.Models;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]/[action]")]
  [ApiController]
  public class SettingsController : ControllerRoot
  {


    public SettingsController(Contexts.MainContext mainContext) : base(mainContext)
    {
    }

    [HttpPost]
    public Response<bool> Post(string key, [FromBody] string value)
    {
      var service = new Services.Config.SettingService(Context, CurrentAdmin);

      LogService.Write("Update Config", string.Format("{0}:{1}", key, value));
      service.Save(new KeyValuePair<string, string>(key, value));

      return new Response<bool>(true);
    }

    [HttpGet]
    public Response<KeyValuePair<string, string>> Get()
    {
      var service = new Services.Config.SettingService(Context, CurrentAdmin);

      return new Response<KeyValuePair<string, string>>(service.Get());
    }

  }
}