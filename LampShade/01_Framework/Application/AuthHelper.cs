﻿
using System.Security.Claims;
using _01_Framework.Infrastructure;
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

        public string? CurrentAccountRole()
        {
            if (IsAuthenticated())
            {
                return _contextAccessor.HttpContext.User
                    .Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            }

            return null;
        }

        public AuthViewModel currentAccountInfo()
        {
            var result=new AuthViewModel();
            if (!IsAuthenticated())
            {
                return result;
            }
            var claims=_contextAccessor.HttpContext.User.Claims.ToList();
            result.Id = Convert.ToInt64(claims.FirstOrDefault(x => x.Type == "AccountId").Value);
            result.UserName = claims.FirstOrDefault(x => x.Type == "UserName").Value;
            result.RoleId = Convert.ToInt64(claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);
            result.FullName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            result.Role = Roles.GetRoleBy(result.RoleId);
            return result;
        }
    }
}
