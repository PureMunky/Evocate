using System;
using System.Collections.Generic;
using Tete.Models.Relationships;

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

    public UserTopicVM UserTopic { get; set; }

    public int OpenMentorships { get; set; }

    public List<MentorshipVM> Mentorships { get; set; }

    public List<Link> Links { get; set; }
    public List<Keyword> Keywords { get; set; }

    public string ShortDescription
    {
      get
      {
        string rtnDes = this.Description;
        int cutOff = rtnDes.IndexOf(".") + 1;

        if (cutOff > 1)
        {
          rtnDes = rtnDes.Substring(0, cutOff);
        }

        return rtnDes;
      }
    }

    public TopicVM()
    {
      FillData("", "", false, DateTime.UtcNow, Guid.Empty);
    }

    public TopicVM(Topic topic)
    {
      FillData(topic.Name, topic.Description, topic.Elligible, topic.Created, topic.CreatedBy);
      this.TopicId = topic.TopicId;
    }
    public TopicVM(Topic topic, UserTopicVM userTopic)
    {
      FillData(topic.Name, topic.Description, topic.Elligible, topic.Created, topic.CreatedBy);
      this.TopicId = topic.TopicId;
      this.UserTopic = userTopic;
    }

    private void FillData(string Name, string Description, bool Elligible, DateTime Created, Guid CreatedBy)
    {
      this.TopicId = Guid.NewGuid();
      this.Name = Name;
      this.Description = Description;
      this.Elligible = Elligible;
      this.Created = Created;
      this.CreatedBy = CreatedBy;
      this.UserTopic = null;
      this.OpenMentorships = 0;
    }
  }

}