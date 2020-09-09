using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using Tete.Api.Contexts;
using Tete.Models.Logging;
using Tete.Models.Authentication;

namespace Tete.Api.Services.Logging
{

  public class LogService : ServiceBase
  {

    public enum LoggingLayer
    {
      Api = 0,
      Service = 1,
      Database = 2,
      Web = 3
    }

    private string DefaultDomain;
    public LogService(MainContext mainContext, LoggingLayer layer, UserVM Actor)
    {
      this.mainContext = mainContext;
      this.Actor = Actor;
      this.DefaultDomain = layer.ToString();

    }

    public Log New()
    {
      return new Log();
    }

    public IEnumerable<Log> Get()
    {
      var rtnLogs = new List<Log>();

      if (this.Actor.Roles.Contains("Admin"))
      {
        rtnLogs = this.mainContext.Logs.AsNoTracking().OrderByDescending(l => l.Occured).ToList();
      }

      return rtnLogs;
    }

    public Log Get(string Id)
    {
      var rtnLog = new Log();

      if (this.Actor.Roles.Contains("Admin"))
      {
        rtnLog = this.mainContext.Logs.Find(Guid.Parse(Id));
      }

      return rtnLog;
    }

    public void Save(Log Object)
    {
      this.mainContext.Logs.Add(Object);
      this.mainContext.SaveChanges();
    }

    public void Write(string Description, string Data = "", string Domain = "")
    {
      try
      {
        Save(new Log(Description, (this.Actor != null ? this.Actor.UserId : Guid.Empty), Data, (Domain.Length > 0 ? Domain : DefaultDomain)));
      }
      catch
      {

      }
    }

    public Dashboard GetDashboard()
    {
      var rtnDashboard = new Dashboard();
      var startDate = DateTime.UtcNow.AddMonths(-1);

      rtnDashboard.TotalUsers = this.mainContext.Users.AsNoTracking().Count();
      rtnDashboard.ActiveUsers = this.mainContext.Sessions.AsNoTracking().Where(s => s.LastUsed > startDate).Select(s => s.UserId).Distinct().Count();

      rtnDashboard.TotalTopics = this.mainContext.Topics.AsNoTracking().Count();
      rtnDashboard.ActiveTopics = this.mainContext.UserTopics.AsNoTracking().Where(ut => ut.CreatedDate > startDate).Select(ut => ut.TopicId).Distinct().Count();
      rtnDashboard.RecentTopics = this.mainContext.Topics.AsNoTracking().Where(t => t.Created > startDate).Count();

      rtnDashboard.TotalMentorships = this.mainContext.Mentorships.AsNoTracking().Count();
      rtnDashboard.WaitingMentorships = this.mainContext.Mentorships.AsNoTracking().Where(m => m.Active && m.MentorUserId == Guid.Empty).Count();
      rtnDashboard.ActiveMentorships = this.mainContext.Mentorships.AsNoTracking().Where(m => m.Active && m.MentorUserId != Guid.Empty).Count();
      rtnDashboard.CompletedMentorships = this.mainContext.Mentorships.AsNoTracking().Where(m => (!m.Active) && m.LearnerClosed && m.MentorClosed).Count();
      rtnDashboard.CancelledMentorships = this.mainContext.Mentorships.AsNoTracking().Where(m => (!m.Active) && m.MentorUserId == Guid.Empty).Count();

      return rtnDashboard;
    }
  }
}