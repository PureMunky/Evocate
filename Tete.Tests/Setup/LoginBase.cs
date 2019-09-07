using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Tete.Models.Authentication;

namespace Tete.Tests.Setup {
  public abstract class LoginTestBase {
    protected Mock<Tete.Api.Contexts.MainContext> mockContext;
    protected const string testUserName = "helloUserName";
    protected const string testPassword = "testPassword";
    protected const string testToken = "abcd";

    [SetUp]
    public void Setup() {
      var salt = Tete.Api.Helpers.Crypto.NewSalt();
      User testUser = new User() {
        UserName = testUserName,
        Salt = salt
      };

      Login testLogin = new Login() {
        UserId = testUser.Id,
        PasswordHash = Tete.Api.Helpers.Crypto.Hash(testPassword, salt)
      };

      IQueryable<User> users = new List<User> {
        testUser
      }.AsQueryable();

      IQueryable<Login> logins = new List<Login> {
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

      mockContext = Tete.Tests.Setup.MockContext.GetDefaultContext();
      mockContext.Setup(c => c.Users).Returns(mockUsers.Object);
      mockContext.Setup(c => c.Logins).Returns(mockLogins.Object);
      mockContext.Setup(c => c.Sessions).Returns(mockSessions.Object);
    }

  }
}