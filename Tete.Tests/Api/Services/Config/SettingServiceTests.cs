using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Tete.Api.Contexts;
using Tete.Api.Services.Config;
using Tete.Models.Config;
using Tete.Tests.Setup;

namespace Tete.Tests.Api.Services.Config
{
  public class SettingServiceTests : TestBase
  {
    SettingService service;

    [SetUp]
    public void SetupSettings()
    {
      this.service = new SettingService(mockContext.Object, AdminUserVM);
    }

    [Test]
    public void GetTest()
    {
      var result = this.service.Get();

      mockContext.Verify(m => m.Settings, Times.Once);
    }

    [Test]
    public void GetOneTest()
    {
      var setting = this.service.Get(settingKey);

      Assert.AreEqual(settingKey, setting.Key);
      Assert.IsNotNull(setting.Value);
    }

    [Test]
    public void SaveTest()
    {
      var testSetting = new Setting();
      this.service.Save(testSetting);

      mockContext.Verify(m => m.Settings.Add(It.IsAny<Setting>()), Times.Once);
    }

    [Test]
    public void SaveExistingTest()
    {
      var testSetting = new Setting()
      {
        Key = settingKey,
        Value = "newValue"
      };
      this.service.Save(testSetting);

      mockContext.Verify(m => m.Settings.Update(It.IsAny<Setting>()), Times.Once);
    }
  }
}