using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Tete.Models.Authentication;

namespace Tete.Tests.Setup
{
  public abstract class UserTestBase
  {
    protected Mock<Tete.Api.Contexts.MainContext> mockContext;

    protected Guid existingUserId = Guid.NewGuid();

    [SetUp]
    public void Setup()
    {
      User existingUser = new User() {
        Id = existingUserId,
        DisplayName = "test user",
        UserName = "TestUser"
      };
      
      IQueryable<User> users = new List<User> {
        existingUser
      }.AsQueryable();

      var mockUsers = MockContext.MockDBSet<User>(users);
      
      mockContext = Tete.Tests.Setup.MockContext.GetDefaultContext();
      mockContext.Setup(c => c.Users).Returns(mockUsers.Object);
    }

  }
}