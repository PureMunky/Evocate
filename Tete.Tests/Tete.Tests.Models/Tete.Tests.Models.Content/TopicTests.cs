using System;
using NUnit.Framework;
using Tete.Models.Content;

namespace Tete.Tests.Models.Content
{
  public class TopicTests
  {

    [Test]
    public void HasAName()
    {
      var topic = new Topic();

      Assert.IsNull(topic.Name);
    }

    [Test]
    public void HasADecription()
    {
      var topic = new Topic();

      Assert.IsNull(topic.Description);
    }

    [Test]
    public void HasAnElligibleFlag()
    {
      var topic = new Topic();

      Assert.IsFalse(topic.Elligible);
    }

    [Test]
    public void HasACreatedDate()
    {
      var topic = new Topic();

      Assert.AreEqual(new DateTime(), topic.Created);
    }
  }
}