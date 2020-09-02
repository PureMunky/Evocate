using System;
using System.Linq;
using System.Collections.Generic;
using Tete.Api.Contexts;
using Tete.Api.Services.Localization;
using Tete.Models.Content;
using Tete.Models.Authentication;
using Tete.Models.Relationships;

namespace Tete.Api.Services.Content
{
  public class LinkService : ServiceBase
  {

    private UserLanguageService userLanguageService;

    public LinkService(MainContext mainContext, UserVM actor)
    {
      FillData(mainContext, actor);
    }

    public Link SaveLink(Link link)
    {
      var rtnLink = link;

      if (this.Actor.Roles.Contains("Admin"))
      {
        var dbLink = this.mainContext.Links.Where(l => l.LinkId == link.LinkId).FirstOrDefault();

        if (dbLink == null)
        {
          this.mainContext.Links.Add(link);
        }
        else
        {
          dbLink.Name = link.Name;
          dbLink.Destination = link.Destination;
          dbLink.Active = link.Active;
          dbLink.Reviewed = link.Reviewed;
          this.mainContext.Update(dbLink);
        }

        this.mainContext.SaveChanges();

        rtnLink = dbLink;
      }

      return rtnLink;
    }

    public IEnumerable<Link> GetLinks()
    {
      var rtnLinks = new List<Link>();

      if (this.Actor.Roles.Contains("Admin"))
      {
        rtnLinks = this.mainContext.Links.OrderBy(l => l.Name).ToList();
      }

      return rtnLinks;
    }

    private void FillData(MainContext mainContext, UserVM actor)
    {
      this.mainContext = mainContext;
      this.Actor = actor;
      this.userLanguageService = new UserLanguageService(mainContext, actor);
    }
  }
}