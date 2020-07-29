using System;

namespace Tete.Models.Content
{
  public class TopicVM
  {
    public Guid TopicId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool Elligible { get; set; }

    public int MentorshipCount { get; set; }

    public DateTime Created { get; set; }

    public Guid CreatedBy { get; set; }

    public TopicVM()
    {
      FillData("", "", false, DateTime.UtcNow, Guid.Empty);
    }

    public TopicVM(Topic topic)
    {
      FillData(topic.Name, topic.Description, topic.Elligible, topic.Created, topic.CreatedBy);
      this.TopicId = topic.TopicId;
    }

    private void FillData(string Name, string Description, bool Elligible, DateTime Created, Guid CreatedBy)
    {
      this.TopicId = Guid.NewGuid();
      this.Name = Name;
      this.Description = Description;
      this.Elligible = false;
      this.Created = Created;
      this.CreatedBy = CreatedBy;
    }
  }

}