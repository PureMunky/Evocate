using System;
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

    public List<Flag> Get()
    {
      return this.mainContext.Flags;
    }

  }
}