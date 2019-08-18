using NUnit.Framework;
using Tete.Models.Logging;

namespace Tete.Tests.Models.Logging
{

  public class LogTests
  {

    [Test]
    public void HasOccured()
    {
      var l = new Log();

      Assert.IsNotNull(l.Occured);
    }

    [Test]
    public void HasDescription()
    {
      string testDescription = "hello description";
      var l = new Log(testDescription);

      Assert.AreEqual(testDescription, l.Description);
    }

    [Test]
    public void hasMachineName()
    {
      var l = new Log();

      Assert.IsNotEmpty(l.MachineName);
    }

    [Test]
    public void hasStackTrace()
    {
      var l = new Log();

      Assert.IsNotEmpty(l.StackTrace);
    }

  }

}