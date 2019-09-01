using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;

namespace Tete.Tests.Setup
{
  public static class MockContext
  {
    public static Mock<DbSet<T>> MockDBSet<T>(IQueryable<T> list) where T : class
    {
      Mock<DbSet<T>> mockSet = new Mock<DbSet<T>>();
      mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(list.Provider);
      mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(list.Expression);
      mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(list.ElementType);
      mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());

      return mockSet;
    }
  }
}