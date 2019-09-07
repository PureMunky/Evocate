using System.Collections.Generic;
using System.Linq;
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
        UserName = testUserName,
        Password = testPassword
      };

      SessionVM result = this.loginService.Login(login);

      Assert.IsTrue(result.Token.Length > 0);
    }

    [Test]
    public void FailedLoginIncorrectPasswordTest() {
      SessionVM results = this.loginService.Login(new LoginAttempt() {
        UserName = testUserName,
          Password = "notTheCorrectPassword"
      });

      Assert.IsNull(results);
    }

    [Test]
    public void FailedLoginIncorrectUserNameTest() {
      SessionVM results = this.loginService.Login(new LoginAttempt() {
        UserName = "wrongUserName",
          Password = testPassword
      });

      Assert.IsNull(results);
    }

    [Test]
    public void RegisterUserTest() {
      RegistrationAttempt registration = new RegistrationAttempt() {
        UserName = testUserName,
        Email = "test@example.com",
        DisplayName = "def",
        Password = testPassword
      };

      SessionVM session = this.loginService.Register(registration);

      mockContext.Verify(m => m.SaveChanges(), Times.AtLeastOnce);
      Assert.IsTrue(session.Token.Length > 0);
    }

    [Test]
    public void GetUserFromTokenTest() {
      User result = this.loginService.GetUserFromToken(testToken);

      Assert.AreEqual(testUserName, result.UserName);
    }

    [Test]
    public void GetUserFromInvalidTokenTest() {
      User result = this.loginService.GetUserFromToken("InvalidToken");

      Assert.IsNull(result);
    }

    [Test]
    public void GetUserVMFromTokenTest() {
      UserVM result = this.loginService.GetUserVMFromToken(testToken);

      Assert.AreEqual(testUserName, result.UserName);
    }

    [Test]
    public void GetUserVMFromInvalidTokenTest() {
      UserVM result = this.loginService.GetUserVMFromToken("InvalidToken");

      Assert.IsNull(result);
    }
  }
}