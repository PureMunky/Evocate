using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Tete.Api.Helpers;
using Tete.Models.Authentication;
using Tete.Models.Localization;
using Tete.Models.Users;
using Tete.Models.Relationships;
using Tete.Models.Config;
using Tete.Models.Content;

namespace Tete.Tests.Setup
{
  public abstract class TestBase
  {
    protected Mock<Tete.Api.Contexts.MainContext> mockContext;

    protected Guid existingUserId = Guid.NewGuid();

    protected Guid newUserId = Guid.NewGuid();

    protected User adminUser = new User()
    {
      DisplayName = "Administrator",
      UserName = "admin"
    };

    protected string settingKey = "settingKey1";

    public UserVM AdminUserVM
    {
      get
      {
        return new UserVM(adminUser)
        {
          Roles = new List<string>() {
            "Admin"
          }
        };
      }
    }

    public Guid linkId = Guid.NewGuid();
    public Guid existingTopicId = Guid.NewGuid();

    [SetUp]
    public void Setup()
    {

      User existingUser = new User()
      {
        Id = existingUserId,
        DisplayName = "test user",
        UserName = "TestUser"
      };

      User newUser = new User()
      {
        Id = newUserId
      };

      Language english = new Language()
      {
        Name = "English",
        Active = true
      };

      Element welcome = new Element()
      {
        Key = "welcome",
        Text = "Welcome!",
        LanguageId = english.LanguageId
      };

      var userLanguage = new UserLanguage()
      {
        Language = english,
        UserId = existingUserId
      };

      var adminUserLanguage = new UserLanguage()
      {
        Language = english,
        UserId = adminUser.Id
      };

      var userProfile = new Profile(existingUserId);

      var accessRoles = new List<AccessRole>() {
        new AccessRole(existingUserId, "Admin") {
          CreatedBy = existingUserId
        },
        new AccessRole(adminUser.Id, "Admin") {
          CreatedBy = adminUser.Id
        }
      };

      IQueryable<User> users = new List<User> {
        existingUser,
        adminUser,
        newUser
      }.AsQueryable();

      IQueryable<UserLanguage> userLanguages = new List<UserLanguage> {
        userLanguage,
        adminUserLanguage
      }.AsQueryable();

      IQueryable<Profile> userProfiles = new List<Profile>() {
        userProfile,
        new Profile(adminUser.Id)
      }.AsQueryable();

      IQueryable<UserBlock> userBlocks = new List<UserBlock>().AsQueryable();

      IQueryable<Language> languages = new List<Language>()
      {
        english
      }.AsQueryable();

      IQueryable<Element> elements = new List<Element>()
      {
        welcome
      }.AsQueryable();

      IQueryable<AccessRole> userAccessRoles = accessRoles.AsQueryable();

      IQueryable<Setting> settings = new List<Setting>()
      {
        new Setting() {
          Key = settingKey,
          Value = Guid.NewGuid().ToString()
        }
      }.AsQueryable();

      IQueryable<Link> links = new List<Link>()
      {
        new Link() {
          LinkId = linkId
        }
      }.AsQueryable();

      IQueryable<Topic> topics = new List<Topic>()
      {
        new Topic(){
          TopicId = existingTopicId,
          Name = "Existing Topic Name"
        }
      }.AsQueryable();

      var mockUsers = MockContext.MockDBSet<User>(users);
      var mockUserLanguages = MockContext.MockDBSet<UserLanguage>(userLanguages);
      var mockUserProfiles = MockContext.MockDBSet<Profile>(userProfiles);
      var mockUserBlocks = MockContext.MockDBSet<UserBlock>(userBlocks);
      var mockUserAccessRoles = MockContext.MockDBSet<AccessRole>(userAccessRoles);
      var mockLanguages = MockContext.MockDBSet<Language>(languages);
      var mockElements = MockContext.MockDBSet<Element>(elements);
      var mockUserTopics = MockContext.MockDBSet<UserTopic>();
      var mockSettings = MockContext.MockDBSet<Setting>(settings);
      var mockLinks = MockContext.MockDBSet<Link>(links);
      var mockTopics = MockContext.MockDBSet<Topic>(topics);
      var mockTopicLinks = MockContext.MockDBSet<TopicLink>();
      var mockKeywords = MockContext.MockDBSet<Keyword>();
      var mockTopicKeywords = MockContext.MockDBSet<TopicKeyword>();

      mockContext = MockContext.GetDefaultContext();
      mockContext.Setup(c => c.Users).Returns(mockUsers.Object);
      mockContext.Setup(c => c.UserLanguages).Returns(mockUserLanguages.Object);
      mockContext.Setup(c => c.UserProfiles).Returns(mockUserProfiles.Object);
      mockContext.Setup(c => c.UserBlocks).Returns(mockUserBlocks.Object);
      mockContext.Setup(c => c.AccessRoles).Returns(mockUserAccessRoles.Object);
      mockContext.Setup(c => c.Languages).Returns(mockLanguages.Object);
      mockContext.Setup(c => c.Elements).Returns(mockElements.Object);
      mockContext.Setup(c => c.UserTopics).Returns(mockUserTopics.Object);
      mockContext.Setup(c => c.Settings).Returns(mockSettings.Object);
      mockContext.Setup(c => c.Links).Returns(mockLinks.Object);
      mockContext.Setup(c => c.Topics).Returns(mockTopics.Object);
      mockContext.Setup(c => c.TopicLinks).Returns(mockTopicLinks.Object);
      mockContext.Setup(c => c.Keywords).Returns(mockKeywords.Object);
      mockContext.Setup(c => c.TopicKeywords).Returns(mockTopicKeywords.Object);

      mockContext.Setup(c => c.Mentorships)
        .Returns(MockContext.MockDBSet<Mentorship>().Object);

    }

  }
}