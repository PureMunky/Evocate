using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Tete.Api.Services.Users;
using Tete.Tests.Setup;
using Tete.Models.Authentication;
using Tete.Models.Users;

namespace Tete.Tests.Api.Services.Users
{
  public class UserServiceTests : UserTestBase
  {
    private ProfileService profileService;

    [SetUp]
    public void SetupTests()
    {
      this.profileService = new ProfileService(mockContext.Object, adminUser);
    }

    [Test]
    public void SanityTest()
    {
      Assert.IsTrue(true);
    }

    [Test]
    public void GetProfileTest()
    {
      UserVM user = this.profileService.GetUser(existingUserId);

      Assert.AreEqual("TestUser", user.UserName);
    }


    [Test]
    public void EditExistingProfileTest()
    {
      string about = "New about message.";
      UserVM user = this.profileService.GetUser(existingUserId);

      user.Profile.About = about;

      this.profileService.SaveProfile(user.Profile);

      UserVM result = this.profileService.GetUser(existingUserId);

      Assert.AreEqual(about, result.Profile.About);
    }

    [Test]
    public void EditNewProfileTest()
    {
      string about = "hello";
      var profile = new Profile(newUserId);

      profile.About = about;

      this.profileService.SaveProfile(profile);

      mockContext.Verify(c => c.UserProfiles.Add(It.IsAny<Profile>()), Times.Once);
    }

  }

}