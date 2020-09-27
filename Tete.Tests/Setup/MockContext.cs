using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Tete.Api.Contexts;
using Tete.Models.Logging;

namespace Tete.Tests.Setup
{
  public static class MockContext
  {

    public static Mock<DbSet<T>> MockDBSet<T>() where T : class
    {
      return MockDBSet<T>(new List<T>().AsQueryable());
    }

    public static Mock<DbSet<T>> MockDBSet<T>(List<T> list) where T : class
    {
      return MockDBSet<T>(list.AsQueryable());
    }

    public static Mock<DbSet<T>> MockDBSet<T>(IQueryable<T> list) where T : class
    {
      Mock<DbSet<T>> mockSet = new Mock<DbSet<T>>();
      mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(list.Provider);
      mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(list.Expression);
      mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(list.ElementType);
      mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());

      return mockSet;
    }

    public static Mock<MainContext> GetDefaultContext()
    {
      Mock<MainContext> mockContext = new Mock<MainContext>();
      Mock<DbSet<Log>> mockLogs = MockDBSet<Log>(new List<Log>().AsQueryable());
      mockContext.Setup(c => c.Logs).Returns(mockLogs.Object);
      return mockContext;
    }

    public static void TestContext(Mock<MainContext> context)
    {
      context.Verify(m => m.Logs.Add(It.IsAny<Log>()), Times.AtLeastOnce);
      context.Verify(m => m.SaveChanges(), Times.AtLeastOnce);
    }
  }
}