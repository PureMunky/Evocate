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
  public class ProfileServiceTests : UserTestBase
  {
    private ProfileService profileService;
    private UserService userService;

    [SetUp]
    public void SetupTests()
    {
      this.userService = new UserService(mockContext.Object, adminUser);
      this.profileService = new ProfileService(mockContext.Object, this.userService.GetUser(adminUser.Id));
    }

    [Test]
    public void SanityTest()
    {
      Assert.IsTrue(true);
    }

    [Test]
    public void EditExistingProfileTest()
    {
      string about = "New about message.";
      UserVM user = this.userService.GetUser(existingUserId);

      user.Profile.About = about;

      this.profileService.SaveProfile(user.Profile);

      UserVM result = this.userService.GetUser(existingUserId);

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