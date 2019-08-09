using System;

namespace Tete.Models.Content
{
  /// <summary>
  /// The topic that a person can learn or teach.
  /// </summary>
  public class Topic
  {

    /// <summary>
    /// The Name of the topic.
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    /// <summary>
    /// The description of the type of information covered by
    /// the topic.
    /// </summary>
    /// <value></value>
    public string Description { get; set; }

    /// <summary>
    /// Determines if the topic is able to have deacons.
    /// </summary>
    /// <value></value>
    public bool Elligible { get; set; }

    /// <summary>
    /// When the topic was created.
    /// </summary>
    /// <value></value>
    public DateTime Created { get; set; }

  }
}