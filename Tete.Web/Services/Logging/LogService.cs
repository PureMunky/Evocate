using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using Tete.Api.Contexts;
using Tete.Models.Logging;
using Tete.Models.Authentication;

namespace Tete.Api.Services.Logging
{

  public class LogService : ServiceBase
  {

    public enum LoggingLayer
    {
      Api = 0,
      Service = 1,
      Database = 2,
      Web = 3
    }

    private string DefaultDomain;
    public LogService(MainContext mainContext, LoggingLayer layer, UserVM Actor)
    {
      this.mainContext = mainContext;
      this.Actor = Actor;
      this.DefaultDomain = layer.ToString();

    }

    public Log New()
    {
      return new Log();
    }

    public IEnumerable<Log> Get()
    {
      var rtnLogs = new List<Log>();

      if (this.Actor.Roles.Contains("Admin"))
      {
        rtnLogs = this.mainContext.Logs.AsNoTracking().OrderByDescending(l => l.Occured).ToList();
      }

      return rtnLogs;
    }

    public Log Get(string Id)
    {
      var rtnLog = new Log();

      if (this.Actor.Roles.Contains("Admin"))
      {
        rtnLog = this.mainContext.Logs.Find(Guid.Parse(Id));
      }

      return rtnLog;
    }

    public void Save(Log Object)
    {
      this.mainContext.Logs.Add(Object);
      this.mainContext.SaveChanges();
    }

    public void Write(string Description, string Data = "", string Domain = "")
    {
      try
      {
        Save(new Log(Description, (this.Actor != null ? this.Actor.UserId : Guid.Empty), Data, (Domain.Length > 0 ? Domain : DefaultDomain)));
      }
      catch
      {

      }
    }

  }
}