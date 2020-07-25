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
    private UserService userService;

    [SetUp]
    public void SetupTests()
    {
      this.userService = new UserService(mockContext.Object, adminUser);
    }

    [Test]
    public void SanityTest()
    {
      Assert.IsTrue(true);
    }

    [Test]
    public void GetProfileTest()
    {
      UserVM user = this.userService.GetUser(existingUserId);

      Assert.AreEqual("TestUser", user.UserName);
    }

  }

}