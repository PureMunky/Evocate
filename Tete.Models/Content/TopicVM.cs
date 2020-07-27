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

    public TopicVM(Topic topic)
    {
      FillData(topic.TopicId, topic.Name, topic.Description, topic.Elligible, topic.Created);
    }

    private void FillData(Guid TopicId, string Name, string Description, bool Elligible, DateTime Created)
    {
      this.TopicId = TopicId;
      this.Name = Name;
      this.Description = Description;
      this.Elligible = false;
      this.Created = Created;
    }
  }

}