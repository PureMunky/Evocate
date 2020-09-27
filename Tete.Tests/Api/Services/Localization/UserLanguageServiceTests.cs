using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Tete.Api.Services.Localization;
using Tete.Models.Localization;
using Tete.Tests.Setup;

namespace Tete.Tests.Api.Services.Localization
{
  public class UserLanuageServiceTests : TestBase
  {
    private UserLanguageService userLanguageService;

    [SetUp]
    public void SetupTests()
    {
      userLanguageService = new UserLanguageService(mockContext.Object, new Tete.Models.Authentication.UserVM()
      {
        Roles = { "Admin" }
      });
    }

    [Test]
    public void SanityTest()
    {
      Assert.IsTrue(true);
    }

    [Test]
    public void GetUserLanguagesTest()
    {
      List<UserLanguage> userLanguages = this.userLanguageService.GetUserLanguages(existingUserId);

      Assert.AreEqual(1, userLanguages.Count);
    }

    [Test]
    public void SaveUserLanguagesTest()
    {
      var langs = new List<UserLanguage>(){
        new UserLanguage() {
          UserId = existingUserId,
          LanguageId = englishId
        },
        new UserLanguage() {
          UserId = existingUserId,
          LanguageId = spanishId,
          Read = true
        }
      };

      this.userLanguageService.SaveUserLanguages(existingUserId, langs);

      mockContext.Verify(c => c.UserLanguages.Add(It.IsAny<UserLanguage>()), Times.AtLeastOnce);
    }
  }
}