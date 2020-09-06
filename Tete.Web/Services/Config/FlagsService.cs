using System.Collections.Generic;
using Tete.Api.Contexts;
using Tete.Models.Config;
using Tete.Models.Authentication;

namespace Tete.Api.Services.Config
{

  public class FlagService : ServiceBase
  {

    public FlagService(MainContext mainContext, UserVM Actor)
    {
      this.mainContext = mainContext;
      this.Actor = Actor;
    }

    public Flag New()
    {
      return new Flag();
    }

    public IEnumerable<Flag> Get()
    {
      return this.mainContext.Flags;
    }

    public Flag Get(string Id)
    {
      return this.mainContext.Flags.Find(Id);
    }

    public void Save(Flag Object)
    {
      LogService.Write("Saving Flag", Object.ToString());
      this.mainContext.Flags.Add(Object);
      this.mainContext.SaveChanges();
    }

  }
}