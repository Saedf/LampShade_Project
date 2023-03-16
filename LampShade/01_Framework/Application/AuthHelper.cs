﻿
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace _01_Framework.Application
{
    public class AuthHelper:IAuthHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsAuthenticated()
        {
            var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            return claims.Count > 0;
        }

        public void SignIn(AuthViewModel account)
        {
            var claims = new List<Claim>
            {
                new Claim("AccountId",account.Id.ToString()),
                new Claim(ClaimTypes.Name,account.FullName),
                new Claim(ClaimTypes.Role,account.RoleId.ToString()),
                new Claim("UserName",account.UserName),
                new Claim("mobile",account.Mobile),

            };
             var claimsIdentity=new ClaimsIdentity(claims,
                 CookieAuthenticationDefaults.AuthenticationScheme);
             var authProperties = new AuthenticationProperties
             {
                 ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
             };
             _contextAccessor.HttpContext.SignInAsync(
                 CookieAuthenticationDefaults.AuthenticationScheme,
                 new ClaimsPrincipal(claimsIdentity), authProperties);

        }
    }
}
