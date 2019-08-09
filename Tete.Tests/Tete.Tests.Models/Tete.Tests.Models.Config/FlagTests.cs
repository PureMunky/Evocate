using NUnit.Framework;
using Tete.Models.Config;
using System;

namespace Tete.Tests.Models.Config
{

  public class FlagTests
  {

    [Test]
    public void HasKey()
    {
      var flag = new Flag();

      Assert.IsNull(flag.Key);
    }

    [Test]
    public void HasValue()
    {
      var flag = new Flag();

      Assert.IsFalse(flag.Value);
    }

    [Test]
    public void HasData()
    {
      var flag = new Flag();

      Assert.IsNull(flag.Data);
    }
  }
}