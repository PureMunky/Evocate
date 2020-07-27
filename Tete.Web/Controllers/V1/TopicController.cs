using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Tete.Models.Content;
using Tete.Api.Helpers;
using Tete.Web.Models;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]/[action]")]
  [ApiController]
  public class TopicController : ControllerBase
  {

    private Api.Services.Logging.LogService logService;

    private Contexts.MainContext context;

    public TopicController(Contexts.MainContext mainContext)
    {
      this.context = mainContext;
      this.logService = new Services.Logging.LogService(mainContext, Tete.Api.Services.Logging.LogService.LoggingLayer.Api);
    }

    // POST api/values
    [HttpPost]
    public Response<TopicVM> Post([FromBody] TopicVM value)
    {
      var service = new Services.Content.TopicService(this.context, UserHelper.CurrentUser(HttpContext, this.context));
      service.SaveTopic(value);

      return new Response<TopicVM>(value);
    }

    [HttpGet]
    public Response<TopicVM> Search(string searchText)
    {
      var service = new Services.Content.TopicService(this.context, UserHelper.CurrentUser(HttpContext, this.context));

      return new Response<TopicVM>(service.Search(searchText));
    }

  }
}