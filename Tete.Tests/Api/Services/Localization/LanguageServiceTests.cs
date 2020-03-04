using System;
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
  }
}