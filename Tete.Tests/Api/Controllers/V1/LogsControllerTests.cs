using Moq;
using NUnit.Framework;
using Tete.Api.Controllers;
using Tete.Models.Logging;
using Tete.Tests.Setup;

namespace Tete.Tests.Api.Controllers {

  public class LogsControllerTests : LogsBase {

    private LogsController controller;

    [SetUp]
    public void SetupTests() {
      controller = new LogsController(mockContext.Object);
    }

    [Test]
    public void GetAllTest() {
      this.controller.Get();

      mockContext.Verify(c => c.Logs, Times.Once);
    }

    [Test]
    public void GetOneTest() {
      this.controller.Get(logId.ToString());

      mockContext.Verify(c => c.Logs.Find(logId), Times.Once);
    }

    [Test]
    public void PostTest() {
      this.controller.Post(new Log());

      mockContext.Verify(c => c.Logs.Add(It.IsAny<Log>()), Times.Once);
      mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }
  }
}