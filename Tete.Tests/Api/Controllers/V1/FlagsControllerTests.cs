using Moq;
using NUnit.Framework;
using Tete.Api.Contexts;

namespace Tete.Tests.Api.Controllers {
  public class FlagsControllerTests {

    Mock<MainContext> mockContext = Tete.Tests.Setup.MockContext.GetDefaultContext();
    Tete.Api.Controllers.FlagsController controller;

    [SetUp]
    public void Setup() {
      this.controller = new Tete.Api.Controllers.FlagsController(mockContext.Object);
    }

    [Test]
    public void GetTest() {
      this.controller.Get();

      mockContext.Verify(m => m.Flags, Times.Once);
      Tete.Tests.Setup.MockContext.TestContext(mockContext);
    }
  }
}