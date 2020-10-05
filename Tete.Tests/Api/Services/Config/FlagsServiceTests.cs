using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Tete.Api.Contexts;
using Tete.Models.Config;
using Tete.Tests.Setup;

namespace Tete.Tests.Api.Services.Config
{
  public class FlagsServiceTests : FlagBase
  {

    [Test]
    public void GetNewTest()
    {
      var flag = this.service.New();

      Assert.IsNotNull(flag);
    }

    [Test]
    public void GetTest()
    {
      this.service.Get();

      mockContext.Verify(m => m.Flags, Times.Once);
    }

    [Test]
    public void GetOneTest()
    {
      string id = "hello";
      this.service.Get(id);

      mockContext.Verify(m => m.Flags.Find(id), Times.Once);
    }

    [Test]
    public void SaveTest()
    {
      Flag testFlag = new Flag();
      this.service.Save(testFlag);

      mockContext.Verify(m => m.Flags.Add(It.IsAny<Flag>()), Times.Once);
    }
  }
}