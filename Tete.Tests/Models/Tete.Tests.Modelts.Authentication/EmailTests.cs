using NUnit.Framework;
using Tete.Models.Authentication;

namespace Tete.Tests.Models.Authentication
{
  public class EmailTests
  {

    [Test]
    public void HasAnAddress()
    {
      var email = new EmailAddress();

      Assert.IsNull(email.Address);
    }
  }
}