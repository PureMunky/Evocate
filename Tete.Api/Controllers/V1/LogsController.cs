using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tete.Models.Logging;

namespace Tete.Api.Controllers {
  [Route("V1/[controller]")]
  [ApiController]
  public class LogsController : ControllerBase {

    private Api.Services.Logging.LogService service;

    public LogsController(Contexts.MainContext mainContext) {
      this.service = new Services.Logging.LogService(mainContext, "Api");
    }
    // GET api/values
    [HttpGet]
    public IEnumerable<Log> Get() {
      return this.service.Get();
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public ActionResult<Log> Get(string id) {
      return this.service.Get(id);
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody] Log value) {
      this.service.Save(value);
    }

  }
}