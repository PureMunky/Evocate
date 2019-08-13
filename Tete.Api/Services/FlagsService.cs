using System.Collections.Generic;
using Tete.Api.Contexts;
using Tete.Models.Config;

namespace Tete.Api.Services
{

  public class FlagService : IService<Flag>
  {
    private MainContext mainContext;
    public FlagService(MainContext mainContext)
    {
      this.mainContext = mainContext;
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
      this.mainContext.Flags.Add(Object);
      this.mainContext.SaveChanges();
    }

  }
}