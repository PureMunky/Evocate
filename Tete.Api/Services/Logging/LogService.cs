using System.Collections.Generic;
using Tete.Api.Contexts;
using Tete.Models.Logging;

namespace Tete.Api.Services.Logging
{

  public class LogService : IService<Log>
  {
    private MainContext mainContext;
    public LogService(MainContext mainContext)
    {
      this.mainContext = mainContext;
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
      return this.mainContext.Logs.Find(Id);
    }

    public void Save(Log Object)
    {
      this.mainContext.Logs.Add(Object);
      this.mainContext.SaveChanges();
    }

  }
}