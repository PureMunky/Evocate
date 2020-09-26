using System;
using Moq;
using NUnit.Framework;
using Tete.Api.Contexts;
using Tete.Api.Services.Content;
using Tete.Models.Content;
using Tete.Tests.Setup;

namespace Tete.Tests.Api.Services.Content
{
  public class TopicServiceTests : TestBase
  {
    TopicService service;

    [SetUp]
    public void SetupSettings()
    {
      this.service = new TopicService(mockContext.Object, AdminUserVM);
    }

    // [Test]
    // public void GetTest()
    // {
    //   var result = this.service.GetTopics();

    //   mockContext.Verify(m => m.Topics, Times.Once);
    // }

    [Test]
    public void SaveTopicTest()
    {
      var newTopic = new TopicVM()
      {
        Name = "New Topic"
      };
      this.service.SaveTopic(newTopic);

      mockContext.Verify(m => m.Topics.Add(It.IsAny<Topic>()), Times.Once);
    }

    [Test]
    public void SaveNewTopicTest()
    {
      var newTopic = new TopicVM()
      {
        TopicId = Guid.Empty,
        Name = "Empty Guid Topic"
      };
      this.service.SaveTopic(newTopic);

      mockContext.Verify(m => m.Topics.Add(It.IsAny<Topic>()), Times.Once);
    }

    [Test]
    public void SaveExistingTest()
    {
      var existingTopic = new TopicVM()
      {
        TopicId = existingTopicId,
        Name = "Existing Topic"
      };
      this.service.SaveTopic(existingTopic);

      mockContext.Verify(m => m.Topics.Update(It.IsAny<Topic>()), Times.Once);
    }

    [Test]
    public void SearchTest()
    {
      var result = this.service.Search("topic");

      var e = result.GetEnumerator();
      e.MoveNext();
      mockContext.Verify(m => m.Topics, Times.Once);
      Assert.IsTrue(e.Current != null);
    }
  }
}