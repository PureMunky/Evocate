using System;
using Microsoft.EntityFrameworkCore;

namespace Tete.Api.Contexts
{
  public class MainContext : DbContext
  {

    public DbSet<Tete.Models.Config.Flag> Flags { get; set; }
    public DbSet<Tete.Models.Config.Setting> Settings { get; set; }

    public MainContext(DbContextOptions options) : base(options)
    {
      Console.WriteLine("hello");
      Database.Migrate();
    }

  }
}