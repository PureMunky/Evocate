using Microsoft.AspNetCore.Mvc;
using Tete.Models.Config;
using Tete.Web.Models;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]")]
  [ApiController]
  public class FlagsController : ControllerRoot
  {

    // TODO: update to current controller structure.

    public FlagsController(Contexts.MainContext mainContext) : base(mainContext)
    {
    }

    // GET api/values
    [HttpGet]
    public Response<Flag> Get()
    {
      var service = new Services.Config.FlagService(Context, CurrentUser);

      return new Response<Flag>(service.Get());
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public Response<Flag> Get(string id)
    {
      var service = new Services.Config.FlagService(Context, CurrentUser);
      return new Response<Flag>(service.Get(id));
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody] Flag value)
    {
      var service = new Services.Config.FlagService(Context, CurrentAdmin);
      service.Save(value);
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put([FromBody] Flag value)
    {
      var service = new Services.Config.FlagService(Context, CurrentAdmin);
      service.Save(value);
    }

  }
}