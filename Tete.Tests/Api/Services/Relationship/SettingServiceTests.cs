using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Tete.Api.Services.Relationships;
using Tete.Models.Relationships;
using Tete.Tests.Setup;

namespace Tete.Tests.Api.Services.Relationship
{
  public class MentorshipServiceTests : TestBase
  {
    MentorshipService service;

    [SetUp]
    public void SetupMentorships()
    {
      this.service = new MentorshipService(mockContext.Object, AdminUserVM);
    }

    [Test]
    public void GetTest()
    {
      var result = this.service.GetMentorship(mentorshipId);

      mockContext.Verify(m => m.Mentorships, Times.Once);
    }

    [Test]
    public void RegisterLearnerTest()
    {
      this.service.RegisterLearner(existingUserId, existingTopicId);

      mockContext.Verify(m => m.Mentorships, Times.AtLeastOnce);
    }

    [Test]
    public void RegisterNewLearnerTest()
    {
      this.service.RegisterLearner(adminUser.Id, existingTopicId);

      mockContext.Verify(m => m.Mentorships.Add(It.IsAny<Mentorship>()), Times.Once);
    }

    [Test]
    public void RegisterMentorTest()
    {
      this.service.RegisterMentor(existingUserId, existingTopicId);

      mockContext.Verify(m => m.UserTopics, Times.AtLeastOnce);
    }

    [Test]
    public void ClaimNextMentorshipTest()
    {
      var mentorship = this.service.ClaimNextMentorship(adminUser.Id, existingTopicId);

      Assert.IsNotNull(mentorship);
    }
  }
}