using NUnit.Framework;
using Moq;
using Tete.Api.Services.Logging;

namespace Tete.Tests.Api.Services.Logging
{

  public class LogServiceTests
  {

    Mock<Tete.Api.Contexts.MainContext> mockContext;
    LogService logService;
    const string Domain = "TEST";

    [SetUp]
    public void Setup()
    {
      mockContext = new Mock<Tete.Api.Contexts.MainContext>();
      logService = new LogService(mockContext.Object, Domain);
    }

    [Test]
    public void SetupTest()
    {
      Assert.IsTrue(true);
    }

    [Test]
    public void NewTest()
    {
      var expected = new Tete.Models.Logging.Log();
      var log = this.logService.New();

      Assert.AreEqual(expected.Data, log.Data);
      Assert.AreNotEqual(expected.Occured, log.Occured);
    }
  }

}