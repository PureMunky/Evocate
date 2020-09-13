using System;
using Moq;
using NUnit.Framework;
using Tete.Api.Services.Authentication;
using Tete.Models.Authentication;
using Tete.Tests.Setup;

namespace Tete.Tests.Api.Services.Authentication
{
  public class LoginServiceTests : LoginTestBase
  {

    private LoginService loginService;

    [SetUp]
    public void SetupTests()
    {
      loginService = new LoginService(mockContext.Object);
    }

    [Test]
    public void LoginTest()
    {
      var login = new LoginAttempt()
      {
        UserName = existingUserName,
        Password = testPassword
      };

      SessionVM result = this.loginService.Login(login);

      mockContext.Verify(c => c.Users, Times.Once);
      mockContext.Verify(c => c.Logins, Times.Once);
      Assert.IsTrue(result.Token.Length > 0);
    }

    [Test]
    public void FailedLoginIncorrectPasswordTest()
    {
      SessionVM results = this.loginService.Login(new LoginAttempt()
      {
        UserName = existingUserName,
        Password = "notTheCorrectPassword"
      });

      Assert.IsNull(results);
    }

    [Test]
    public void FailedLoginIncorrectUserNameTest()
    {
      var login = new LoginAttempt()
      {
        UserName = "wrongUserName",
        Password = testPassword
      };

      SessionVM results = this.loginService.Login(login);

      Assert.IsNull(results);
    }

    [Test]
    public void GetUserFromTokenTest()
    {
      var result = this.loginService.GetUserFromToken(existingUserToken);

      Assert.AreEqual(existingUserName, result.UserName);
    }

    [Test]
    public void GetUserFromInvalidTokenTest()
    {
      var result = this.loginService.GetUserFromToken("InvalidToken");

      Assert.IsNull(result);
    }

    [Test]
    public void GetUserVMFromTokenTest()
    {
      UserVM result = this.loginService.GetUserVMFromToken(existingUserToken);

      Assert.AreEqual(existingUserName, result.UserName);
    }

    [Test]
    public void GetUserVMFromInvalidTokenTest()
    {
      UserVM result = this.loginService.GetUserVMFromToken("InvalidToken");

      Assert.IsNull(result);
    }
  }
}