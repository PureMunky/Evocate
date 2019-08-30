using NUnit.Framework;
using Tete.Api.Helpers;

namespace Tete.Tests.Api.Helpers
{

  public class CryptoTests
  {

    [Test]
    public void GenerateSalt()
    {
      var salt = Crypto.NewSalt();
      var salt2 = Crypto.NewSalt();

      Assert.AreNotEqual(salt, salt2);
    }

    [Test]
    public void HashPassword()
    {
      var salt = Crypto.NewSalt();
      string password = "testPassword";
      string hash = Crypto.Hash(password, salt);
      string hash2 = Crypto.Hash(password, salt);

      Assert.AreNotEqual(password, hash);
      Assert.AreEqual(hash, hash2);
      Assert.IsTrue(hash.Length > 0);
    }
  }
}