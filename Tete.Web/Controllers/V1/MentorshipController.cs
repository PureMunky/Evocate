using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Tete.Models.Relationships;
using Tete.Models.Users;
using Tete.Api.Helpers;
using Tete.Web.Models;

namespace Tete.Api.Controllers
{
  [Route("V1/[controller]/[action]")]
  [ApiController]
  public class MentorshipController : ControllerRoot
  {

    private Api.Services.Logging.LogService logService;

    public MentorshipController(Contexts.MainContext mainContext) : base(mainContext)
    {
      this.logService = new Services.Logging.LogService(mainContext, Tete.Api.Services.Logging.LogService.LoggingLayer.Api);
    }

    // POST api/values
    [HttpPost]
    public Response<Mentorship> Post([FromBody] Mentorship value)
    {
      var service = new Services.Relationships.MentorshipService(Context, CurrentUser);
      // service.SaveMentorship(value);

      return new Response<Mentorship>(value);
    }

    [HttpGet]
    public Response<MentorshipVM> GetUserMentorships(Guid UserId)
    {
      var service = new Services.Relationships.MentorshipService(Context, CurrentUser);

      return new Response<MentorshipVM>(service.GetUserMentorships(UserId));
    }

    [HttpGet]
    public Response<MentorshipVM> GetMentorship(Guid MentorshipId)
    {
      var service = new Services.Relationships.MentorshipService(Context, CurrentUser);

      return new Response<MentorshipVM>(service.GetMentorship(MentorshipId));
    }

    [HttpPost]
    public Response<MentorshipVM> SetContactDetails([FromBody] ContactUpdate ContactUpdate)
    {
      var service = new Services.Relationships.MentorshipService(Context, CurrentUser);

      return new Response<MentorshipVM>(service.SetContactDetails(ContactUpdate));
    }

    [HttpPost]
    public Response<MentorshipVM> CloseMentorship([FromBody] Evaluation evaluation)
    {
      var service = new Services.Relationships.MentorshipService(Context, CurrentUser);

      return new Response<MentorshipVM>(service.CloseMentorship(evaluation));
    }

    [HttpPost]
    public Response<MentorshipVM> CancelMentorship(Guid MentorshipId)
    {
      var service = new Services.Relationships.MentorshipService(Context, CurrentUser);

      return new Response<MentorshipVM>(service.CancelMentorship(MentorshipId));
    }

  }
}