using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Tete.Api.Services.Localization;
using Tete.Models.Localization;
using Tete.Tests.Setup;

namespace Tete.Tests.Api.Services.Localization
{
  public class UserLanuageServiceTests : LocalizationTestBase
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
      List<UserLanguage> userLanguages = this.userLanguageService.GetUserLanguages(userId);

      Assert.AreEqual(1, userLanguages.Count);
      Assert.AreEqual(2, userLanguages[0].Language.Elements.Count);
    }
  }
}