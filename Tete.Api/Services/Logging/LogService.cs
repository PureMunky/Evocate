using System.Collections.Generic;
using Tete.Api.Contexts;
using Tete.Models.Logging;
using System;

namespace Tete.Api.Services.Logging
{

  public class LogService : IService<Log>
  {
    private MainContext mainContext;
    private string DefaultDomain;
    public LogService(MainContext mainContext, string Domain)
    {
      this.mainContext = mainContext;
      this.DefaultDomain = Domain;
    }

    public Log New()
    {
      return new Log();
    }

    public IEnumerable<Log> Get()
    {
      return this.mainContext.Logs;
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