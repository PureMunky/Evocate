using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Tete.Models.Config;

namespace Tete.Tests.Setup
{
  public abstract class FlagBase : TestBase
  {
    protected Mock<DbSet<Flag>> mockFlags;
    protected Tete.Api.Services.Config.FlagService service;

    [SetUp]
    public void SetupFlags()
    {
      var testFlag = new Flag();
      IQueryable<Flag> flags = new List<Flag>() {
        testFlag
      }.AsQueryable();

      mockContext = Tete.Tests.Setup.MockContext.GetDefaultContext();
      var mockFlags = Tete.Tests.Setup.MockContext.MockDBSet<Flag>(flags);
      mockContext.Setup(c => c.Flags).Returns(mockFlags.Object);
      this.service = new Tete.Api.Services.Config.FlagService(mockContext.Object, AdminUserVM);
    }
  }
}