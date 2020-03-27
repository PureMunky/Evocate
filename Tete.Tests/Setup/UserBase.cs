using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Tete.Models.Authentication;
using Tete.Models.Localization;
using Tete.Models.Users;

namespace Tete.Tests.Setup
{
  public abstract class UserTestBase
  {
    protected Mock<Tete.Api.Contexts.MainContext> mockContext;

    protected Guid existingUserId = Guid.NewGuid();

    [SetUp]
    public void Setup()
    {
      User existingUser = new User() {
        Id = existingUserId,
        DisplayName = "test user",
        UserName = "TestUser"
      };

      var userLanguage = new UserLanguage(){
        Language = new Language(),
        UserId = existingUserId
      };
      
      var userProfile = new Profile(existingUserId);

      IQueryable<User> users = new List<User> {
        existingUser
      }.AsQueryable();

      IQueryable<UserLanguage> userLanguages = new List<UserLanguage> {
        userLanguage
      }.AsQueryable();

      IQueryable<Profile> userProfiles = new List<Profile>() {
        userProfile
      }.AsQueryable();


      var mockUsers = MockContext.MockDBSet<User>(users);
      var mockUserLanguages = MockContext.MockDBSet<UserLanguage>(userLanguages);      
      var mockUserProfiles = MockContext.MockDBSet<Profile>(userProfiles);

      mockContext = Tete.Tests.Setup.MockContext.GetDefaultContext();
      mockContext.Setup(c => c.Users).Returns(mockUsers.Object);
      mockContext.Setup(c => c.UserLanguages).Returns(mockUserLanguages.Object);
          mockContext.Setup(c => c.UserProfiles).Returns(mockUserProfiles.Object);
    }

  }
}