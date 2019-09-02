using NUnit.Framework;
using Moq;
using Tete.Api.Services.Logging;
using Tete.Models.Logging;
using Tete.Tests.Setup;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace Tete.Tests.Api.Services.Logging
{

  public class LogServiceTests
  {

    Mock<Tete.Api.Contexts.MainContext> mockContext;
    LogService logService;
    const string Domain = "TEST";
    Guid logId = Guid.NewGuid();
    Mock<DbSet<Log>> mockLogs;

    [SetUp]
    public void Setup()
    {
      Log testLog = new Log()
      {
        Data = "testData",
        Description = "testDescription",
        Domain = Domain,
        LogId = logId
      };

      IQueryable<Log> logs = new List<Log>
      {
        testLog
      }.AsQueryable();

      mockLogs = MockContext.MockDBSet<Log>(logs);
      mockContext = new Mock<Tete.Api.Contexts.MainContext>();
      mockContext.Setup(c => c.Logs).Returns(mockLogs.Object);

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
  }

}