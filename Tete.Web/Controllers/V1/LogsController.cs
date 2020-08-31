using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tete.Models.Logging;
using Tete.Web.Models;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]")]
  [ApiController]
  public class LogsController : ControllerRoot
  {

    private Api.Services.Logging.LogService service;

    public LogsController(Contexts.MainContext mainContext) : base(mainContext)
    {
      if (CurrentAdmin != null)
      {
        this.service = new Services.Logging.LogService(mainContext, Tete.Api.Services.Logging.LogService.LoggingLayer.Api);
      }
    }
    // GET api/values
    [HttpGet]
    public Response<Log> Get()
    {
      return new Response<Log>(this.service.Get());
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public Response<Log> Get(string id)
    {
      return new Response<Log>(this.service.Get(id));
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody] Log value)
    {
      this.service.Save(value);
    }

  }
}