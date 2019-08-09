using System;

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
    public string Data { get; set; }
  }
}