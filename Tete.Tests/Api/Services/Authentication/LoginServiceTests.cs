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
    public void LogoutTest()
    {
      this.loginService.Logout(existingUserToken);

      Assert.IsTrue(true);
      mockContext.Verify(c => c.Sessions, Times.AtLeastOnce);
      mockContext.Verify(c => c.SaveChanges(), Times.AtLeastOnce);
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

    [Test]
    public void GetUserFromOrphanTokenTest()
    {
      var user = this.loginService.GetUserFromToken(orphanToken);

      Assert.IsNull(user);
    }

    [Test]
    public void GetNewAnonymousSessionsTest()
    {
      var sessionVM = this.loginService.GetNewAnonymousSession();

      Assert.IsTrue(sessionVM != null);
    }

    [Test]
    public void GetUserVMFromUsernameTest()
    {
      var user = this.loginService.GetUserVMFromUsername(existingUserName, AdminUserVM);

      Assert.AreEqual(existingUserName, user.UserName);
    }

    [Test]
    public void ResetPasswordTest()
    {
      var response = this.loginService.ResetPassword(existingUserToken, "abcde1234%");

      Assert.IsTrue(response.Successful);
    }

    [Test]
    public void ResetNewPasswordTest()
    {
      var response = this.loginService.ResetPassword(guestToken, "abcde1234%");

      Assert.IsTrue(response.Successful);
    }

    [Test]
    public void ResetEmptyPasswordTest()
    {
      var response = this.loginService.ResetPassword(existingUserToken, null);

      Assert.IsFalse(response.Successful);
      Assert.AreEqual(3, response.Messages.Count);
    }

    [Test]
    public void ResetShortPasswordTest()
    {
      var response = this.loginService.ResetPassword(existingUserToken, "abd#3ls");

      Assert.IsFalse(response.Successful);
      Assert.AreEqual(1, response.Messages.Count);
    }

    [Test]
    public void ResetNonSpecialPasswordTest()
    {
      var response = this.loginService.ResetPassword(existingUserToken, "3124llls");

      Assert.IsFalse(response.Successful);
      Assert.AreEqual(1, response.Messages.Count);
    }

    [Test]
    public void ResetNonNumberPasswordTest()
    {
      var response = this.loginService.ResetPassword(existingUserToken, "abdh#$le");

      Assert.IsFalse(response.Successful);
      Assert.AreEqual(1, response.Messages.Count);
    }

    [Test]
    public void UpdateUserNameTest()
    {
      var response = this.loginService.UpdateUserName(existingUserToken, "newusername");

      Assert.IsTrue(response.Successful);
    }

    [Test]
    public void UpdateEmptyUserNameTest()
    {
      var response = this.loginService.UpdateUserName(existingUserToken, null);

      Assert.IsFalse(response.Successful);
      Assert.AreEqual(1, response.Messages.Count);
    }

    [Test]
    public void UpdateShortUserNameTest()
    {
      var response = this.loginService.UpdateUserName(existingUserToken, "");

      Assert.IsFalse(response.Successful);
      Assert.AreEqual(1, response.Messages.Count);
    }

    [Test]
    public void UpdateTakenUserNameTest()
    {
      var response = this.loginService.UpdateUserName(existingUserToken, existingUserName);

      Assert.IsFalse(response.Successful);
      Assert.AreEqual(1, response.Messages.Count);
    }

    [Test]
    public void RegisterNewLoginTest()
    {
      var response = this.loginService.RegisterNewLogin(existingUserToken, new LoginAttempt()
      {
        UserName = "newusername",
        Password = "newPassword123!@#"
      });

      Assert.IsTrue(response.Successful);
    }

    [Test]
    public void DeleteAccountTest()
    {
      this.loginService.DeleteAccount(existingUserId, AdminUserVM);
      Assert.True(true);
    }
  }
}