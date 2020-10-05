using System.Linq;
using Moq;
using NUnit.Framework;
using Tete.Api.Services.Logging;
using Tete.Models.Logging;
using Tete.Tests.Setup;

namespace Tete.Tests.Api.Services.Logging
{

  public class LogServiceTests : LogsBase
  {

    LogService logService;

    [SetUp]
    public void SetUp()
    {
      logService = new LogService(mockContext.Object, Tete.Api.Services.Logging.LogService.LoggingLayer.Service, AdminUserVM);
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

    [Test]
    public void GetTest()
    {
      var result = this.logService.Get();

      Assert.AreEqual(1, result.Count());
    }

    [Test]
    public void GetOneTest()
    {
      var result = this.logService.Get(logId.ToString());

      mockLogs.Verify(m => m.Find(logId), Times.Once);
    }

    [Test]
    public void SaveTest()
    {
      var log = new Log();
      this.logService.Save(log);

      mockLogs.Verify(m => m.Add(log), Times.Once);
      mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }

    [Test]
    public void WriteTest()
    {
      this.logService.Write("test");
      mockLogs.Verify(m => m.Add(It.IsAny<Log>()), Times.Once);
      mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }

    [Test]
    public void GetDashboardTest()
    {
      var dashboard = this.logService.GetDashboard();

      Assert.IsNotNull(dashboard);
    }
  }

}