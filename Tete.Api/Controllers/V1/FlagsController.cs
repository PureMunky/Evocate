using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tete.Models.Config;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]")]
  [ApiController]
  public class FlagsController : ControllerBase
  {

    private Api.Services.Service<Flag> service;
    private Api.Services.Logging.LogService logService;

    public FlagsController(Contexts.MainContext mainContext)
    {
      this.service = new Services.Service<Flag>(mainContext.Flags);
      this.logService = new Services.Logging.LogService(mainContext);
    }
    // GET api/values
    [HttpGet]
    public IEnumerable<Flag> Get()
    {
      this.logService.Save(new Models.Logging.Log("Call Flags.Get()"));
      return this.service.Get();
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public ActionResult<Flag> Get(string id)
    {

      return this.service.Get(id);
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody] Flag value)
    {
      this.service.Save(value);
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
