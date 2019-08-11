using Microsoft.EntityFrameworkCore;

namespace Tete.Job
{
  public class MainContext : DbContext
  {

    public DbSet<Tete.Models.Config.Flag> Flags { get; set; }
    public DbSet<Tete.Models.Config.Setting> Settings { get; set; }

    public MainContext()
    {
      // Database.Migrate();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer("Data Source=tete");
    }

  }
}