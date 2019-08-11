using System;

namespace Tete.Models.Relationships
{

  /// <summary>
  /// Defines the relationship between a user and a topic.
  /// </summary>
  public class UserTopic
  {

    /// <summary>
    /// The user Id that this relationship describes.
    /// </summary>
    /// <value></value>
    public Guid UserId {get; set;}

    /// <summary>
    /// The topic Id that this relationship describes.
    /// </summary>
    /// <value></value>
    public Guid TopicId {get; set;}

    /// <summary>
    /// The level of knowledge that the user has for the topic.
    /// </summary>
    /// <value></value>
    public TopicStatus Status {get; set;}

    /// <summary>
    /// The date that the user initially enrolled in the topic.
    /// </summary>
    /// <value></value>
    public DateTime StartDate {get; set;}
  }
}