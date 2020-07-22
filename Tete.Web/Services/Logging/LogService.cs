using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using Tete.Api.Contexts;
using Tete.Models.Logging;

namespace Tete.Api.Services.Logging
{

  public class LogService : IService<Log>
  {

    public enum LoggingLayer
    {
      Api = 0,
      Service = 1,
      Database = 2,
      Web = 3
    }

    private MainContext mainContext;
    private string DefaultDomain;
    public LogService(MainContext mainContext, LoggingLayer layer)
    {
      this.mainContext = mainContext;
      this.DefaultDomain = layer.ToString();
    }

    public Log New()
    {
      return new Log();
    }

    public IEnumerable<Log> Get()
    {
      return this.mainContext.Logs.AsNoTracking().OrderByDescending(l => l.Occured);
    }

    public Log Get(string Id)
    {
      return this.mainContext.Logs.Find(Guid.Parse(Id));
    }

    public void Save(Log Object)
    {
      this.mainContext.Logs.Add(Object);
      this.mainContext.SaveChanges();
    }

    public void Write(string Description, string Data = "", string Domain = "")
    {
      Save(new Log(Description, Data, (Domain.Length > 0 ? Domain : DefaultDomain)));
    }

  }
}