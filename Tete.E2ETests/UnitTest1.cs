using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System;
using System.Reflection;

namespace Tests
{
  public class Tests
  {
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void PerformSearchExample()
    {
      using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
      {
        driver.Navigate().GoToUrl("http://www.google.com");
        var el = driver.FindElement(By.Name("q"));
        el.SendKeys("test search");

        driver.FindElement(By.Name("btnK")).Click();

        driver.Quit();
      }

    }
  }
}