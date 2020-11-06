using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tete.Web.Models;
using Tete.Models.Config;

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
    public Response<bool> Post([FromBody] Setting setting)
    {
      var service = new Services.Config.SettingService(Context, CurrentAdmin);

      LogService.Write("Update Config", string.Format("{0}:{1}", setting.Key, setting.Value));
      service.Save(setting);

      return new Response<bool>(true);
    }

    [HttpPost]
    public Response<bool> Delete([FromBody] Setting setting)
    {
      var service = new Services.Config.SettingService(Context, CurrentAdmin);

      LogService.Write("Deleted Config", string.Format("{0}:{1}", setting.Key, setting.Value));
      service.Delete(setting.Key);

      return new Response<bool>(true);
    }

    [HttpGet]
    public Response<KeyValuePair<string, string>> Get()
    {
      var service = new Services.Config.SettingService(Context, CurrentUser);

      return new Response<KeyValuePair<string, string>>(service.Get());
    }

  }
}