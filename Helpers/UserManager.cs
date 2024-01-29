using IISprojekt3.Data.IisDbContext;
using IISprojekt3.Models.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IISprojekt3.Helpers
{
  public class UserManager
  {
    private const KeyDerivationPrf PRF = KeyDerivationPrf.HMACSHA256;
    private const int ITERATIONS = 10000;
    private const int SALT_SIZE = 16;
    private const int SUBKEY_SIZE = 32;

    public async Task<bool> SignIn(HttpContext httpContext, IisdbContext iisdb, string userName, string password)
    {
      var user = iisdb.User.FirstOrDefault(x => x.Name == userName);
      if (user != null)
      {
        if (VerifyHashedPassword(user.Password, password))
        {
          ClaimsIdentity identity = new ClaimsIdentity(GetUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);
          await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
          return true;
        }
      }
      return false;
    }

    public async Task SignOut(HttpContext httpContext)
    {
      await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    private IEnumerable<Claim> GetUserClaims(User user)
    {
      List<Claim> claims = new List<Claim>();

      claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
      claims.Add(new Claim(ClaimTypes.Name, user.Name));
      claims.AddRange(GetUserRoleClaims(user));
      return claims;
    }

    private IEnumerable<Claim> GetUserRoleClaims(User user)
    {
      List<Claim> claims = new List<Claim>();

      claims.Add(new Claim(ClaimTypes.Role, Enum.GetName(typeof(EUserType), user.UserType).ToUpper()));
      return claims;
    }

    public async Task UpdateNameAsync(HttpContext httpContext, string username)
    {
      var identity = httpContext.User.Identity as ClaimsIdentity;
      if (identity is null)
        return;

      var existingClaim = identity.FindFirst(ClaimTypes.Name);
      if (existingClaim != null)
        identity.RemoveClaim(existingClaim);

      identity.AddClaim(new Claim(ClaimTypes.Name, username));
      await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
    }

    public static string HashPassword(string password)
    {
      var salt = new byte[SALT_SIZE];
      new Random(DateTime.Now.Millisecond).NextBytes(salt);
      var subkey = KeyDerivation.Pbkdf2(password, salt, PRF, ITERATIONS, SUBKEY_SIZE);

      var outputBytes = new byte[salt.Length + subkey.Length];
      Buffer.BlockCopy(salt, 0, outputBytes, 0, salt.Length);
      Buffer.BlockCopy(subkey, 0, outputBytes, SALT_SIZE, subkey.Length);
      return Convert.ToBase64String(outputBytes);
    }

    public static bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
      var decodedHashedPassword = Convert.FromBase64String(hashedPassword);

      var salt = new byte[SALT_SIZE];
      Buffer.BlockCopy(decodedHashedPassword, 0, salt, 0, salt.Length);
      var expectedSubkey = new byte[SUBKEY_SIZE];
      Buffer.BlockCopy(decodedHashedPassword, salt.Length, expectedSubkey, 0, expectedSubkey.Length);

      var actualSubkey = KeyDerivation.Pbkdf2(providedPassword, salt, PRF, ITERATIONS, SUBKEY_SIZE);
      return actualSubkey.SequenceEqual(expectedSubkey);
    }
  }
}
