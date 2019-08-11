using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Config
{

  /// <summary>
  /// A system-wide setting.
  /// </summary>
  public class Setting
  {
    
    /// <summary>
    /// The setting PK.
    /// </summary>
    /// <value></value>
    [Key]
    [Required]
    [MaxLength(30)]
    public string Key { get; set; }

    /// <summary>
    /// The value of the setting.
    /// </summary>
    /// <value></value>
    [MaxLength(100)]
    public string Value { get; set; }
  }
}