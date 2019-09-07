using System;
using Moq;
using NUnit.Framework;
using Tete.Api.Services.Authentication;
using Tete.Models.Authentication;
using Tete.Tests.Setup;

namespace Tete.Tests.Api.Services.Authentication {
  public class LoginServiceTests : LoginTestBase {

    private LoginService loginService;

    [SetUp]
    public void SetupTests() {
      loginService = new LoginService(mockContext.Object);
    }

    [Test]
    public void LoginTest() {
      var login = new LoginAttempt() {
        UserName = existingUserName,
        Password = testPassword
      };

      SessionVM result = this.loginService.Login(login);

      Assert.IsTrue(result.Token.Length > 0);
    }

    [Test]
    public void FailedLoginIncorrectPasswordTest() {
      SessionVM results = this.loginService.Login(new LoginAttempt() {
        UserName = existingUserName,
          Password = "notTheCorrectPassword"
      });

      Assert.IsNull(results);
    }

    [Test]
    public void FailedLoginIncorrectUserNameTest() {
      var login = new LoginAttempt() {
        UserName = "wrongUserName",
        Password = testPassword
      };

      SessionVM results = this.loginService.Login(login);

      Assert.IsNull(results);
    }

    [Test]
    public void RegisterUserTest() {
      RegistrationAttempt registration = new RegistrationAttempt() {
        UserName = newUserName,
        Email = "test@example.com",
        DisplayName = "def",
        Password = testPassword
      };

      this.loginService.Register(registration);

      mockContext.Verify(m => m.SaveChanges(), Times.AtLeastOnce);
    }

    [Test]
    public void AlreadyRegisteredUserTest() {
      // Test if you try to register a user that already exists that it errors.
      var registration = new RegistrationAttempt() {
        UserName = existingUserName,
        Email = "test@example.com",
        DisplayName = "anything",
        Password = testPassword
      };

      try {
        this.loginService.Register(registration);
        Assert.Fail();
      } catch (Exception) {
        Assert.Pass();
      }
    }

    [Test]
    public void GetUserFromTokenTest() {
      User result = this.loginService.GetUserFromToken(existingUserToken);

      Assert.AreEqual(existingUserName, result.UserName);
    }

    [Test]
    public void GetUserFromInvalidTokenTest() {
      User result = this.loginService.GetUserFromToken("InvalidToken");

      Assert.IsNull(result);
    }

    [Test]
    public void GetUserVMFromTokenTest() {
      UserVM result = this.loginService.GetUserVMFromToken(existingUserToken);

      Assert.AreEqual(existingUserName, result.UserName);
    }

    [Test]
    public void GetUserVMFromInvalidTokenTest() {
      UserVM result = this.loginService.GetUserVMFromToken("InvalidToken");

      Assert.IsNull(result);
    }
  }
}