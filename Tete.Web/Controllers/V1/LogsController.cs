using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tete.Models.Logging;
using Tete.Web.Models;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]/[action]")]
  [ApiController]
  public class LogsController : ControllerRoot
  {


    public LogsController(Contexts.MainContext mainContext) : base(mainContext)
    {
    }
    // GET api/values
    [HttpGet]
    public Response<Log> Get()
    {
      var service = new Services.Logging.LogService(Context, Tete.Api.Services.Logging.LogService.LoggingLayer.Api, CurrentAdmin);

      return new Response<Log>(service.Get());
    }

    [HttpGet]
    public Response<Dashboard> Dashboard()
    {
      var service = new Services.Logging.LogService(Context, Tete.Api.Services.Logging.LogService.LoggingLayer.Api, CurrentAdmin);
      var dashboard = service.GetDashboard();
      return new Response<Dashboard>(dashboard);
    }

  }
}