using System;
using Microsoft.EntityFrameworkCore;

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

    // Topics
    public virtual DbSet<Tete.Models.Content.Topic> Topics { get; set; }

    public MainContext(DbContextOptions options) : base(options)
    {
      Console.WriteLine("Initializing MainContext");
      Database.Migrate();
    }

    public MainContext()
    {

    }

  }
}