using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Tete.Models.Content;
using Tete.Models.Relationships;
using Tete.Web.Models;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]/[action]")]
  [ApiController]
  public class LinkController : ControllerRoot
  {

    private Api.Services.Logging.LogService logService;

    public LinkController(Contexts.MainContext mainContext) : base(mainContext)
    {
      this.logService = new Services.Logging.LogService(mainContext, Tete.Api.Services.Logging.LogService.LoggingLayer.Api);
    }

    [HttpPost]
    public Response<Link> Post([FromBody] Link value)
    {
      var service = new Services.Content.LinkService(Context, CurrentAdmin);

      return new Response<Link>(service.SaveLink(value));
    }

    [HttpGet]
    public Response<Link> Get()
    {
      var service = new Services.Content.LinkService(Context, CurrentAdmin);

      return new Response<Link>(service.GetLinks());
    }

  }
}