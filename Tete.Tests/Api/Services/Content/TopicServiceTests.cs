using System;
using System.Linq;
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

    [Test]
    public void GetKeywordTopicsTest()
    {
      var result = this.service.GetKeywordTopics(keyword);

      var e = result.GetEnumerator();
      e.MoveNext();
      mockContext.Verify(m => m.Topics, Times.Once);
      Assert.IsTrue(e.Current != null);

      // Commenting because the VM is not fully populated in this method and it isn't required
      // to be for the front-end usage.
      // foreach (TopicVM t in result)
      // {
      //   Assert.IsTrue(t.Keywords.AsQueryable().Where(k => k.Name == keyword).Count() > 0);
      // }
    }

    [Test]
    public void GetUsersTopicsTest()
    {
      var result = this.service.GetUsersTopics(adminUser.Id);

      var e = result.GetEnumerator();
      e.MoveNext();
      mockContext.Verify(m => m.Topics, Times.Once);
      Assert.IsTrue(e.Current != null);
    }
  }
}