using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using Tete.Api.Contexts;
using Tete.Models.Config;
using Tete.Models.Authentication;

namespace Tete.Api.Services.Config
{

  public class SettingService : ServiceBase
  {

    public SettingService(MainContext mainContext, UserVM Actor)
    {
      this.mainContext = mainContext;
      this.Actor = Actor;
    }

    public Flag New()
    {
      return new Flag();
    }

    public Dictionary<string, string> Get()
    {
      return this.mainContext.Settings.AsNoTracking().OrderBy(s => s.Key).ToDictionary(s => s.Key, s => s.Value);
    }

    public Setting Get(string key)
    {
      return this.mainContext.Settings.AsNoTracking().Where(s => s.Key == key).FirstOrDefault();
    }

    public void Save(Setting setting)
    {
      if (this.Actor.Roles.Contains("Admin"))
      {
        var dbSetting = this.mainContext.Settings.Where(s => s.Key == setting.Key).FirstOrDefault();

        if (dbSetting != null)
        {
          dbSetting.Value = setting.Value;
          dbSetting.LastUpdated = DateTime.UtcNow;
          dbSetting.LastUpdatedBy = this.Actor.UserId;
          this.mainContext.Settings.Update(dbSetting);
        }
        else
        {
          var newSetting = new Setting()
          {
            Key = setting.Key,
            Value = setting.Value,
            LastUpdatedBy = this.Actor.UserId
          };

          this.mainContext.Settings.Add(newSetting);
        }

        this.mainContext.SaveChanges();
      }
    }
  }
}