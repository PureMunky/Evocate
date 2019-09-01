using NUnit.Framework;
using Moq;
using Tete.Api.Services.Authentication;
using Tete.Models.Authentication;

namespace Tete.Tests.Api.Services.Authentication
{
  public class LoginServiceTests
  {

    Mock<Tete.Api.Contexts.MainContext> mockContext;
    LoginService loginService;

    [SetUp]
    public void Setup()
    {
      mockContext = new Mock<Tete.Api.Contexts.MainContext>();
      loginService = new LoginService(mockContext.Object);
    }

    [Test]
    public void LoginTest()
    {
      var login = new LoginAttempt()
      {
        UserName = "helloUserName",
        Password = "fakePassword"
      };

      SessionVM result = this.loginService.Login(login);

      Assert.AreNotEqual("", result);
    }
  }
}