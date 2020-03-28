using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Tete.Models.Logging;

namespace Tete.Tests.Setup {
  public abstract class LogsBase : TestBase {
    protected const string Domain = "TEST";
    protected Guid logId = Guid.NewGuid();
    protected Mock<DbSet<Log>> mockLogs;

    [SetUp]
    public void SetupLogs() {
      Log testLog = new Log() {
        Data = "testData",
        Description = "testDescription",
        Domain = Domain,
        LogId = logId
      };

      IQueryable<Log> logs = new List<Log> {
        testLog
      }.AsQueryable();

      mockLogs = MockContext.MockDBSet<Log>(logs);
      mockContext.Setup(c => c.Logs).Returns(mockLogs.Object);
    }
  }
}