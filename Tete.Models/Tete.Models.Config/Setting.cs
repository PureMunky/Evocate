using System;

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
    public string Key { get; set; }

    /// <summary>
    /// The value of the setting.
    /// </summary>
    /// <value></value>
    public string Value { get; set; }
  }
}