using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Content
{
  /// <summary>
  /// The topic that a person can learn or teach.
  /// </summary>
  public class Topic
  {
    public Guid TopicId { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public bool Elligible { get; set; }

    public DateTime Created { get; set; }

    [Required]
    public Guid CreatedBy { get; set; }

    public Topic()
    {
      FillData("", "", DateTime.UtcNow);
    }

    private void FillData(string Name, string Description, DateTime Created)
    {
      this.TopicId = Guid.NewGuid();
      this.Name = Name;
      this.Description = Description;
      this.Elligible = false;
      this.Created = Created;
    }
  }

}