using Microsoft.AspNetCore.Mvc;
using Tete.Models.Config;
using Tete.Web.Models;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]")]
  [ApiController]
  public class FlagsController : ControllerBase
  {

    private Api.Services.Config.FlagService service;
    private Api.Services.Logging.LogService logService;

    public FlagsController(Contexts.MainContext mainContext)
    {
      this.service = new Services.Config.FlagService(mainContext);
      this.logService = new Services.Logging.LogService(mainContext, Tete.Api.Services.Logging.LogService.LoggingLayer.Api);
    }

    // GET api/values
    [HttpGet]
    public Response<Flag> Get()
    {
      this.logService.Write("Api Get All Flags");
      return new Response<Flag>(this.service.Get());
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public Response<Flag> Get(string id)
    {
      this.logService.Write("Get Flag", id);
      return new Response<Flag>(this.service.Get(id));
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody] Flag value)
    {
      this.logService.Write("Post Flag", value.ToString());
      this.service.Save(value);
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put([FromBody] Flag value)
    {
      this.logService.Write("Put Flag", value.Key);
      this.service.Save(value);
    }

  }
}