using Email.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Email.Authentication
{
    public class HttpAuthentication
    {
        public ClaimsPrincipal GetLoginClaim(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.EmailAddress));
            claims.Add(new Claim(ClaimTypes.Name, user.PersonalInfo.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.PersonalInfo.LastName));
            claims.Add(new Claim("Password", user.Password));
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principalClaim = new ClaimsPrincipal(claimsIdentity);
            return principalClaim;
        }
    }
}
