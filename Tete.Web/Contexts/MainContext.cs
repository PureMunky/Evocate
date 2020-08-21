using System;
using Microsoft.EntityFrameworkCore;
using Tete.Models;

namespace Tete.Api.Contexts
{
  public class MainContext : DbContext
  {

    // Core Functionality
    public virtual DbSet<Tete.Models.Config.Flag> Flags { get; set; }
    public virtual DbSet<Tete.Models.Config.Setting> Settings { get; set; }
    public virtual DbSet<Tete.Models.Logging.Log> Logs { get; set; }
    public virtual DbSet<Tete.Models.Authentication.User> Users { get; set; }
    public virtual DbSet<Tete.Models.Authentication.Login> Logins { get; set; }
    public virtual DbSet<Tete.Models.Authentication.Session> Sessions { get; set; }

    // Localization/Languages
    public virtual DbSet<Tete.Models.Localization.Language> Languages { get; set; }
    public virtual DbSet<Tete.Models.Localization.Element> Elements { get; set; }
    public virtual DbSet<Tete.Models.Localization.UserLanguage> UserLanguages { get; set; }

    // User Details
    public virtual DbSet<Tete.Models.Users.Profile> UserProfiles { get; set; }
    public virtual DbSet<Tete.Models.Authentication.AccessRole> AccessRoles { get; set; }
    public virtual DbSet<Tete.Models.Users.Evaluation> Evaluations { get; set; }

    // Topics
    public virtual DbSet<Tete.Models.Content.Topic> Topics { get; set; }
    public virtual DbSet<Tete.Models.Relationships.Mentorship> Mentorships { get; set; }
    public virtual DbSet<Tete.Models.Relationships.UserTopic> UserTopics { get; set; }
    public virtual DbSet<Tete.Models.Content.TopicLink> TopicLinks { get; set; }
    public virtual DbSet<Tete.Models.Content.Link> Links { get; set; }
    public virtual DbSet<Tete.Models.Content.Keyword> Keywords { get; set; }
    public virtual DbSet<Tete.Models.Content.TopicKeyword> TopicKeywords { get; set; }

    public MainContext(DbContextOptions options) : base(options)
    {
    }

    public MainContext()
    {

    }

    public void Migrate()
    {
      Database.Migrate();
    }

  }
}