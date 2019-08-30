using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Tete.Api.Helpers
{
  public class Crypto
  {

    public static string Hash(string plaintext, byte[] salt)
    {
      return Convert.ToBase64String(KeyDerivation.Pbkdf2(
        password: plaintext,
        salt: salt,
        prf: KeyDerivationPrf.HMACSHA1,
        iterationCount: 10000,
        numBytesRequested: 256 / 8
      ));
    }

    public static byte[] NewSalt()
    {
      byte[] salt = new byte[128 / 8];

      using (var rng = RandomNumberGenerator.Create())
      {
        rng.GetBytes(salt);
      }

      return salt;
    }
  }
}