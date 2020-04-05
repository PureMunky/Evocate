using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Tete.Api.Helpers;
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
      languageService = new LanguageService(mockContext.Object, new Tete.Models.Authentication.UserVM()
      {
        Roles = new List<string>() {
          "Admin"
        }
      });
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
      foreach (Element e in languages[0].Elements)
      {
        if (e.Key == testKey)
        {
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

    [Test]
    public void UpdateTest()
    {
      Language l = this.languageService.GetLanguages()[0];

      l.Elements.Add(new Element()
      {
        Key = "net"
      });

      this.languageService.Update(l);

      mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }

    [Test]
    public void CreateAccessFailureTest()
    {
      languageService = new LanguageService(mockContext.Object, new Tete.Models.Authentication.UserVM());

      try
      {
        this.languageService.CreateLanguage("test");
      }
      catch (Exception e)
      {
        Assert.AreEqual("Incorrect user permissions.", e.Message);
      }
    }
  }
}