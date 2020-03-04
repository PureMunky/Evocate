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

    [SetUp]
    public void Setup()
    {
      Language language = new Language()
      {
        Name = languageName,
        Active = true
      };

      IQueryable<Language> languages = new List<Language> {
        language
      }.AsQueryable();

      var mockLanguages = MockContext.MockDBSet<Language>(languages);

      mockContext = Tete.Tests.Setup.MockContext.GetDefaultContext();
      mockContext.Setup(c => c.Languages).Returns(mockLanguages.Object);
    }
  }
}