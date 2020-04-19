using System;
using Tete.Models.Authentication;
using Tete.Api.Contexts;
using Microsoft.AspNetCore.Http;
using Tete.Api.Services.Authentication;

namespace Tete.Api.Helpers
{
  public static class UserHelper
  {

    public static UserVM CurrentUser(HttpContext current, MainContext mainContext)
    {
      var token = current.Request.Cookies["Tete.SessionToken"];
      var user = new LoginService(mainContext).GetUserVMFromToken(token);

      return user;
    }

  }
}