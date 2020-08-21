using Tete.Api.Contexts;
using Tete.Models.Authentication;

namespace Tete.Api.Services
{
  public abstract class ServiceBase
  {
    protected MainContext mainContext;

    protected UserVM Actor;

    protected Content.TopicService TopicService
    {
      get
      {
        return new Content.TopicService(this.mainContext, this.Actor);
      }
    }

    protected Relationships.MentorshipService MentorshipService
    {
      get
      {
        return new Relationships.MentorshipService(this.mainContext, this.Actor);
      }
    }
  }
}