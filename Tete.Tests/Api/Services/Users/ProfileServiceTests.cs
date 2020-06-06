using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Tete.Api.Services.Users;
using Tete.Tests.Setup;
using Tete.Models.Authentication;

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
    public void EditProfileTest()
    {
      string about = "New about message.";
      UserVM user = this.profileService.GetUser(existingUserId);

      user.Profile.About = about;

      // I think I might get rid of the VM idea.
      // As long as I'm building security around
      // updates then it shouldn't be a problem
      // knowing any of he Guids associated with things.
      this.profileService.SaveProfile(user.Profile);

      UserVM result = this.profileService.GetUser(existingUserId);

      Assert.AreEqual(about, result.Profile.About);

    }

  }

}