using NUnit.Framework;
using Tete.Api.Services.Logging;

namespace Tete.Tests.Api.Services.Logging
{

  public class LogServiceTests
  {

    [Test]
    public void NewTest()
    {
      var service = new LogService(new Tete.Api.Contexts.MainContext());
    }

  }

}