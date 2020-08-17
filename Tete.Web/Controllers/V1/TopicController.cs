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
  public class TopicController : ControllerRoot
  {

    private Api.Services.Logging.LogService logService;

    public TopicController(Contexts.MainContext mainContext) : base(mainContext)
    {
      this.logService = new Services.Logging.LogService(mainContext, Tete.Api.Services.Logging.LogService.LoggingLayer.Api);
    }

    [HttpPost]
    public Response<TopicVM> Post([FromBody] TopicVM value)
    {
      var service = new Services.Content.TopicService(Context, CurrentUser);
      service.SaveTopic(value);

      return new Response<TopicVM>(value);
    }

    [HttpPost]
    public Response<bool> RegisterLearner(Guid UserId, Guid TopicId)
    {
      var service = new Services.Relationships.MentorshipService(Context, CurrentUser);
      service.RegisterLearner(UserId, TopicId);

      return new Response<bool>(true);
    }

    [HttpPost]
    public Response<bool> RegisterMentor(Guid UserId, Guid TopicId)
    {
      var service = new Services.Relationships.MentorshipService(Context, CurrentUser);
      service.RegisterMentor(UserId, TopicId);

      return new Response<bool>(true);
    }

    [HttpPost]
    public Response<MentorshipVM> ClaimNextMentorship(Guid UserId, Guid TopicId)
    {
      var service = new Services.Relationships.MentorshipService(Context, CurrentUser);

      return new Response<MentorshipVM>(service.ClaimNextMentorship(UserId, TopicId));
    }

    [HttpGet]
    public Response<TopicVM> Search(string searchText)
    {
      var service = new Services.Content.TopicService(Context, CurrentUser);

      return new Response<TopicVM>(service.Search(searchText));
    }

    [HttpGet]
    public Response<TopicVM> GetTopic(Guid topicId)
    {
      var service = new Services.Content.TopicService(Context, CurrentUser);

      return new Response<TopicVM>(service.GetTopic(topicId));
    }

    [HttpGet]
    public Response<TopicVM> GetTopTopics()
    {
      var service = new Services.Content.TopicService(Context, CurrentUser);

      return new Response<TopicVM>(service.GetTopTopics());
    }

    [HttpGet]
    public Response<TopicVM> GetNewestTopics()
    {
      var service = new Services.Content.TopicService(Context, CurrentUser);

      return new Response<TopicVM>(service.GetNewestTopics());
    }

    [HttpGet]
    public Response<TopicVM> GetWaitingTopics()
    {
      var service = new Services.Content.TopicService(Context, CurrentUser);

      return new Response<TopicVM>(service.GetWaitingTopics());
    }

    [HttpGet]
    public Response<TopicVM> GetUserTopics(Guid userId)
    {
      var service = new Services.Content.TopicService(Context, CurrentUser);

      return new Response<TopicVM>(service.GetUserTopics(userId));
    }

  }
}