using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Tete.Models.Users;
using Tete.Api.Helpers;
using Tete.Web.Models;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]/[action]")]
  [ApiController]
  public class ProfileController : ControllerRoot
  {


    public ProfileController(Contexts.MainContext mainContext) : base(mainContext)
    {
    }

    // POST api/values
    [HttpPost]
    public Response<Profile> Post([FromBody] Profile value)
    {
      var service = new Services.Users.ProfileService(Context, CurrentUser);
      service.SaveProfile(value);

      return new Response<Profile>(value);
    }

  }
}