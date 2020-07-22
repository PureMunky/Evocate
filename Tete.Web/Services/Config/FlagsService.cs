using System.Collections.Generic;
using Tete.Api.Contexts;
using Tete.Models.Config;

namespace Tete.Api.Services.Config
{

  public class FlagService : IService<Flag>
  {
    private MainContext mainContext;
    private Logging.LogService logService;

    public FlagService(MainContext mainContext)
    {
      this.mainContext = mainContext;
      this.logService = new Logging.LogService(mainContext, Tete.Api.Services.Logging.LogService.LoggingLayer.Service);
    }

    public Flag New()
    {
      this.logService.Write("Get Flag", "New");
      return new Flag();
    }

    public IEnumerable<Flag> Get()
    {
      this.logService.Write("Get Flags", "All");
      return this.mainContext.Flags;
    }

    public Flag Get(string Id)
    {
      this.logService.Write("Getting Flag", Id);
      return this.mainContext.Flags.Find(Id);
    }

    public void Save(Flag Object)
    {
      this.logService.Write("Saving Flag", Object.ToString());
      this.mainContext.Flags.Add(Object);
      this.mainContext.SaveChanges();
    }

  }
}