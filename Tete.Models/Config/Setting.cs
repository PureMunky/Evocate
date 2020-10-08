using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Config
{

  public class Setting
  {

    [Key]
    [Required]
    public string Key { get; set; }

    public string Value { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastUpdated { get; set; }

    public Guid LastUpdatedBy { get; set; }

    public Setting()
    {
      this.Key = "";
      this.Value = "";
      this.Created = DateTime.UtcNow;
      this.LastUpdated = DateTime.UtcNow;
      this.LastUpdatedBy = Guid.Empty;
    }
  }
}