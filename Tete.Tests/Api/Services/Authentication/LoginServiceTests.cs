using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tete.Api.Services.Authentication;
using Tete.Models.Authentication;
using Tete.Tests.Setup;
using System.Collections.Generic;
using System.Linq;

namespace Tete.Tests.Api.Services.Authentication
{
  public class LoginServiceTests
  {

    private Mock<Tete.Api.Contexts.MainContext> mockContext;
    private LoginService loginService;
    private const string testUserName = "helloUserName";
    private const string testPassword = "testPassword";
    private const string testToken = "abcd";

    [SetUp]
    public void Setup()
    {
      var salt = Tete.Api.Helpers.Crypto.NewSalt();
      User testUser = new User()
      {
        UserName = testUserName,
        Salt = salt
      };

      Login testLogin = new Login()
      {
        UserId = testUser.Id,
        PasswordHash = Tete.Api.Helpers.Crypto.Hash(testPassword, salt)
      };

      IQueryable<User> users = new List<User>
      {
        testUser
      }.AsQueryable();

      IQueryable<Login> logins = new List<Login>
      {
        testLogin
      }.AsQueryable();

      IQueryable<Session> sessions = new List<Session> {
        new Session() {
          UserId = testUser.Id,
          Token = testToken
        }
      }.AsQueryable();

      var mockUsers = MockContext.MockDBSet<User>(users);
      var mockLogins = MockContext.MockDBSet<Login>(logins);
      var mockSessions = MockContext.MockDBSet<Session>(sessions);

      mockContext = new Mock<Tete.Api.Contexts.MainContext>();
      mockContext.Setup(c => c.Users).Returns(mockUsers.Object);
      mockContext.Setup(c => c.Logins).Returns(mockLogins.Object);
      mockContext.Setup(c => c.Sessions).Returns(mockSessions.Object);

      loginService = new LoginService(mockContext.Object);
    }

    [Test]
    public void LoginTest()
    {
      var login = new LoginAttempt()
      {
        UserName = testUserName,
        Password = testPassword
      };

      SessionVM result = this.loginService.Login(login);

      Assert.IsTrue(result.Token.Length > 0);
    }

    [Test]
    public void FailedLoginIncorrectPasswordTest()
    {
      SessionVM results = this.loginService.Login(new LoginAttempt()
      {
        UserName = testUserName,
        Password = "notTheCorrectPassword"
      });

      Assert.IsNull(results);
    }

    [Test]
    public void FailedLoginIncorrectUserNameTest()
    {
      SessionVM results = this.loginService.Login(new LoginAttempt()
      {
        UserName = "wrongUserName",
        Password = testPassword
      });

      Assert.IsNull(results);
    }

    [Test]
    public void RegisterUserTest()
    {
      RegistrationAttempt registration = new RegistrationAttempt()
      {
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
    public void GetUserFromTokenTest()
    {
      User result = this.loginService.GetUserFromToken(testToken);

      Assert.AreEqual(testUserName, result.UserName);
    }

    [Test]
    public void GetUserFromInvalidTokenTest()
    {
      User result = this.loginService.GetUserFromToken("InvalidToken");

      Assert.IsNull(result);
    }

    [Test]
    public void GetUserVMFromTokenTest()
    {
      UserVM result = this.loginService.GetUserVMFromToken(testToken);

      Assert.AreEqual(testUserName, result.UserName);
    }

    [Test]
    public void GetUserVMFromInvalidTokenTest()
    {
      UserVM result = this.loginService.GetUserVMFromToken("InvalidToken");

      Assert.IsNull(result);
    }
  }
}