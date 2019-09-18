using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System;
using System.Reflection;

namespace Tests
{
  public class RegisterNewUserTests
  {
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void RegisterNewUserTest()
    {
      using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
      {
        // generate new details on each run.
        // test assumed login on screen "Hello, XXXXXX".
        driver.Navigate().GoToUrl("localhost:5001/Login/Register");
        driver.FindElement(By.Name("userName")).SendKeys("testUserName");
        driver.FindElement(By.Name("userDisplayName")).SendKeys("testDisplayName");
        driver.FindElement(By.Name("userEmail")).SendKeys("test@example.com");
        driver.FindElement(By.Name("userPassword")).SendKeys("testPassword");

        driver.FindElement(By.Id("submit")).Click();

        driver.FindElement(By.TagName("h1")).Click();

        //driver.Quit();
      }

    }
  }
}