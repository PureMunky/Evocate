using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Tete.Models.Localization;

namespace Tete.Tests.Setup
{
  public abstract class LocalizationTestBase
  {
    protected Mock<Tete.Api.Contexts.MainContext> mockContext;
    protected const string languageName = "English";
    protected const bool languageActive = true;
    protected Guid userId = Guid.NewGuid();
    protected string testKey = "TestKey";
    protected string testText = "TextText";

    [SetUp]
    public void Setup()
    {
      Language language = new Language()
      {
        Name = languageName,
        Active = true,
        Elements = new List<Element> {
          new Element() { Key = testKey, Text = testText},
          new Element() { Key = "TestKey2", Text = "TestText2"}
        }
      };

      UserLanguage userLanguage = new UserLanguage()
      {
        UserId = userId,
        Language = language,
        LanguageId = language.LanguageId
      };

      IQueryable<Language> languages = new List<Language> {
        language
      }.AsQueryable();

      IQueryable<UserLanguage> userLanguages = new List<UserLanguage> {
        userLanguage
      }.AsQueryable();

      var mockLanguages = MockContext.MockDBSet<Language>(languages);
      var mockUserLanguages = MockContext.MockDBSet<UserLanguage>(userLanguages);

      mockContext = Tete.Tests.Setup.MockContext.GetDefaultContext();
      mockContext.Setup(c => c.Languages).Returns(mockLanguages.Object);
      mockContext.Setup(c => c.UserLanguages).Returns(mockUserLanguages.Object);
    }
  }
}