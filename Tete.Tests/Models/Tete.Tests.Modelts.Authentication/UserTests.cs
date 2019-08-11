using System;
using NUnit.Framework;
using Tete.Models.Authentication;

namespace Tete.Tests.Models.Authentication
{
  public class UserTests
  {

    [Test]
    public void HasAnAddress()
    {
      var user = new User();

      Assert.IsNull(user.Email);
    }

    [Test]
    public void HasADisplayName()
    {
      var user = new User();

      Assert.IsNull(user.DisplayName);
    }

    [Test]
    public void HasAnId()
    {
      var user = new User();

      Assert.AreEqual(new Guid(), user.Id);
    }
  }
}