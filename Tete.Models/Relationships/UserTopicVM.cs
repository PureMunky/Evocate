using System;
using System.ComponentModel.DataAnnotations;
using Tete.Models.Content;

namespace Tete.Models.Relationships
{

  public class UserTopicVM : UserTopic
  {
    public string StatusText
    {
      get
      {
        return this.Status.ToString();
      }

    }
    public UserTopicVM() : base() { }

    public UserTopicVM(UserTopic userTopic)
    {
      this.UserTopicID = userTopic.UserTopicID;
      this.Status = userTopic.Status;
      this.TopicId = userTopic.TopicId;
      this.UserId = userTopic.UserId;
      this.CreatedDate = userTopic.CreatedDate;
    }
  }

}