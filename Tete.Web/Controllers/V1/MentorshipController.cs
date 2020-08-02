using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Tete.Models.Relationships;
using Tete.Api.Helpers;
using Tete.Web.Models;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]/[action]")]
  [ApiController]
  public class MentorshipController : ControllerBase
  {

    private Api.Services.Logging.LogService logService;

    private Contexts.MainContext context;

    public MentorshipController(Contexts.MainContext mainContext)
    {
      this.context = mainContext;
      this.logService = new Services.Logging.LogService(mainContext, Tete.Api.Services.Logging.LogService.LoggingLayer.Api);
    }

    // POST api/values
    [HttpPost]
    public Response<Mentorship> Post([FromBody] Mentorship value)
    {
      var service = new Services.Relationships.MentorshipService(this.context, UserHelper.CurrentUser(HttpContext, this.context));
      // service.SaveMentorship(value);

      return new Response<Mentorship>(value);
    }

    // [HttpGet]
    // public Response<Mentorship> GetMentorship(Guid MentorshipId)
    // {
    //   var service = new Services.Relationships.MentorshipService(this.context, UserHelper.CurrentUser(HttpContext, this.context));

    //   return new Response<Mentorship>(service.ge(MentorshipId));
    // }

  }
}