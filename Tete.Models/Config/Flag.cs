using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Config
{

  /// <summary>
  /// A feature flag to be used to flip features on/off.
  /// </summary>
  public class Flag
  {

    /// <summary>
    /// The name to reference this flag by.
    /// </summary>
    /// <value></value>
    [Key]
    [MaxLength(30)]
    [Required]
    public string Key { get; set; }

    /// <summary>
    /// The boolean that flips if the flag is on/off.
    /// </summary>
    /// <value></value>
    public bool Value { get; set; }

    /// <summary>
    /// Additional data that can be set for the flag.
    /// </summary>
    /// <value></value>
    [MaxLength(200)]
    public string Data { get; set; }
  }
}