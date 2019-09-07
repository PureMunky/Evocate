using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Tete.Api.Contexts;
using Tete.Models.Config;

namespace Tete.Tests.Api.Controllers {
  public class FlagsControllerTests {

    Mock<MainContext> mockContext;
    Tete.Api.Controllers.FlagsController controller;

    [SetUp]
    public void Setup() {
      var testFlag = new Flag();
      IQueryable<Flag> flags = new List<Flag>() {
        testFlag
      }.AsQueryable();

      mockContext = Tete.Tests.Setup.MockContext.GetDefaultContext();
      var mockFlags = Tete.Tests.Setup.MockContext.MockDBSet<Flag>(flags);
      mockContext.Setup(c => c.Flags).Returns(mockFlags.Object);
      this.controller = new Tete.Api.Controllers.FlagsController(mockContext.Object);
    }

    [TearDown]
    public void TearDown() {
      Tete.Tests.Setup.MockContext.TestContext(mockContext);
    }

    [Test]
    public void GetTest() {
      this.controller.Get();

      mockContext.Verify(m => m.Flags, Times.Once);
    }

    [Test]
    public void GetOneTest() {
      string id = "hello";
      this.controller.Get(id);

      mockContext.Verify(m => m.Flags.Find(id), Times.Once);
    }

    [Test]
    public void PostTest() {
      Flag testFlag = new Flag();
      this.controller.Post(testFlag);

      mockContext.Verify(m => m.Flags.Add(It.IsAny<Flag>()), Times.Once);
    }

    [Test]
    public void PutTest() {
      Flag testFlag = new Flag();
      this.controller.Put(testFlag);

      mockContext.Verify(m => m.Flags.Add(It.IsAny<Flag>()), Times.Once);
    }
  }
}