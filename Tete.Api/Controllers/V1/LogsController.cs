using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tete.Models.Logging;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]")]
  [ApiController]
  public class LogsController : ControllerBase
  {

    private Api.Services.Service<Log> service;

    public LogsController(Contexts.MainContext mainContext)
    {
      this.service = new Services.Service<Log>(mainContext.Logs);
    }
    // GET api/values
    [HttpGet]
    public IEnumerable<Log> Get()
    {
      return this.service.Get();
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public ActionResult<Log> Get(string id)
    {

      return this.service.Get(id);
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody] Log value)
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
