using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Tete.Api.Services.Localization;
using Tete.Models.Localization;
using Tete.Tests.Setup;

namespace Tete.Tests.Api.Services.Localization
{
  public class LanuageServiceTests : LocalizationTestBase
  {
    private LanguageService languageService;

    [SetUp]
    public void SetupTests()
    {
      languageService = new LanguageService(mockContext.Object);
    }

    [Test]
    public void SanityTest()
    {
      Assert.IsTrue(true);
    }

    [Test]
    public void GetLanguagesTest()
    {
      List<Language> languages = this.languageService.GetLanguages();

      Assert.AreEqual(1, languages.Count);
      foreach (Element e in languages[0].Elements) {
        if(e.Key == testKey) {
          Assert.AreEqual(testText, e.Text);
        }
      }
    }

    [Test]
    public void CreateLanguageTest()
    {
      Language l = this.languageService.CreateLanguage("test");

      mockContext.Verify(m => m.SaveChanges(), Times.AtLeastOnce);
      Assert.AreEqual("test", l.Name);
    }

  }
}