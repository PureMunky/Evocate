using System;
using NUnit.Framework;
using Tete.Models.Config;

namespace Tete.Tests.Models.Config
{

  public class SettingTests
  {

    [Test]
    public void HasKey()
    {
      var setting = new Setting();

      Assert.IsNotNull(setting.Key);
    }

    [Test]
    public void HasValue()
    {
      var setting = new Setting();

      Assert.IsNotNull(setting.Value);
    }
  }
}