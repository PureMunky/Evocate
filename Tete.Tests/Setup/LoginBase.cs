using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Tete.Models.Authentication;

namespace Tete.Tests.Setup
{
  public abstract class LoginTestBase
  {
    protected Mock<Tete.Api.Contexts.MainContext> mockContext;
    protected const string newUserName = "helloUserName";
    protected const string testPassword = "testPassword";
    protected const string existingUserToken = "abcd";
    protected const string newUserToken = "efgh";
    protected const string existingUserName = "existingUser";
    protected const string about = "test about";
    protected const string privateAbout = "testing private about";

    [SetUp]
    public void Setup()
    {
      var salt = Tete.Api.Helpers.Crypto.NewSalt();
      User existingUser = new User()
      {
        UserName = existingUserName,
        Salt = salt
      };

      User newUser = new User()
      {
        UserName = newUserName,
        Salt = salt
      };

      Login existingUserLogin = new Login()
      {
        UserId = existingUser.Id,
        PasswordHash = Tete.Api.Helpers.Crypto.Hash(testPassword, salt)
      };

      Login newUserLogin = new Login()
      {
        UserId = newUser.Id,
        PasswordHash = Tete.Api.Helpers.Crypto.Hash(testPassword, salt)
      };

      var accessRoles = new List<AccessRole>() {
        new AccessRole(existingUser.Id, "Admin") {
          CreatedBy = existingUser.Id
        }
      };

      IQueryable<User> users = new List<User> {
        existingUser
      }.AsQueryable();

      IQueryable<Login> logins = new List<Login> {
        existingUserLogin,
        newUserLogin
      }.AsQueryable();

      IQueryable<AccessRole> userAccessRoles = accessRoles.AsQueryable();

      Session existingUserSession = new Session()
      {
        UserId = existingUser.Id,
        Token = existingUserToken
      };

      Session newUserSession = new Session()
      {
        UserId = newUser.Id,
        Token = newUserToken
      };

      IQueryable<Session> sessions = new List<Session> {
        existingUserSession,
        newUserSession
      }.AsQueryable();

      IQueryable<Tete.Models.Localization.UserLanguage> userLanguages = new List<Tete.Models.Localization.UserLanguage> {
        new Tete.Models.Localization.UserLanguage() {

        }
      }.AsQueryable();

      IQueryable<Tete.Models.Users.Profile> userProfiles = new List<Tete.Models.Users.Profile> {
        new Tete.Models.Users.Profile(existingUser.Id) {
          About = about,
          PrivateAbout = privateAbout
        }
      }.AsQueryable();

      var mockUsers = MockContext.MockDBSet<User>(users);
      var mockLogins = MockContext.MockDBSet<Login>(logins);
      var mockSessions = MockContext.MockDBSet<Session>(sessions);
      var mockUserLanguages = MockContext.MockDBSet<Tete.Models.Localization.UserLanguage>(userLanguages);
      var mockUserProfiles = MockContext.MockDBSet<Tete.Models.Users.Profile>(userProfiles);
      var mockUserAccessRoles = MockContext.MockDBSet<AccessRole>(userAccessRoles);

      mockContext = Tete.Tests.Setup.MockContext.GetDefaultContext();
      mockContext.Setup(c => c.Users).Returns(mockUsers.Object);
      mockContext.Setup(c => c.Logins).Returns(mockLogins.Object);
      mockContext.Setup(c => c.Sessions).Returns(mockSessions.Object);
      mockContext.Setup(c => c.UserLanguages).Returns(mockUserLanguages.Object);
      mockContext.Setup(c => c.UserProfiles).Returns(mockUserProfiles.Object);
      mockContext.Setup(c => c.AccessRoles).Returns(mockUserAccessRoles.Object);
    }

  }
}