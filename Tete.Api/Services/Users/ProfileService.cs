using System;
using System.Linq;
using Tete.Api.Contexts;
using Tete.Models;

namespace Tete.Api.Services.Users
{
  public class ProfileService
  {

    private MainContext mainContext;

    public ProfileService(MainContext mainContext)
    {
      this.mainContext = mainContext;
    }

  }
}