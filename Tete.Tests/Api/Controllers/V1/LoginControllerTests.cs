using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Tete.Api.Controllers;
using Tete.Models.Authentication;
using Tete.Tests.Setup;

namespace Tete.Tests.Api.Controllers {

  public class LoginControllerTests : LoginTestBase {

    private LoginController controller;

    [SetUp]
    public void SetupTests() {
      controller = new LoginController(mockContext.Object);
    }

    [TearDown]
    public void TearDown() {
      Tete.Tests.Setup.MockContext.TestContext(mockContext);
    }

    [Test]
    public void LoginTest() {
      var attempt = new LoginAttempt() {
        UserName = testUserName,
        Password = testPassword
      };

      this.controller.Login(attempt);

      mockContext.Verify(c => c.Users, Times.Once);
      mockContext.Verify(c => c.Logins, Times.Once);
    }

    [Test]
    public void RegisterTest() {
      var registration = new RegistrationAttempt() {
        UserName = "newUser",
        Password = "newPassword",
        DisplayName = "newDisplayName",
        Email = "new@example.com"
      };

      this.controller.Register(registration);

      mockContext.Verify(c => c.Users.Add(It.IsAny<User>()), Times.Once);
      mockContext.Verify(c => c.Logins.Add(It.IsAny<Login>()), Times.Once);
    }

    [Test]
    public void CurrentUserTest() {
      this.controller.CurrentUser(testToken);

      mockContext.Verify(c => c.Sessions, Times.Once);
      mockContext.Verify(c => c.Users, Times.Once);
    }
  }
}