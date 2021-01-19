using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NerdStoreEnterprise.WebApp.MVC.Models.Identity;
using NerdStoreEnterprise.WebApp.MVC.Services.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.WebApp.MVC.Controllers
{
    public class IdentityController : MainController
    {
        private readonly IAuthService _authenticationService;

        public IdentityController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync(RegisterUserViewModel registerUserViewModel)
        {
            if (!ModelState.IsValid) return View(registerUserViewModel);

            var response = await _authenticationService.Register(registerUserViewModel);

            if (ResponseHasErrors(response.ResponseResult)) return View(registerUserViewModel);

            await PerformLoginAsync(response);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(UserLoginViewModel userLoginViewModel, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid) return View(userLoginViewModel);

            var response = await _authenticationService.Login(userLoginViewModel);

            if (ResponseHasErrors(response.ResponseResult)) return View(userLoginViewModel);

            await PerformLoginAsync(response);

            if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> LogoutAsync(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task PerformLoginAsync(UserLoginResponse response)
        {
            var token = GetToken(response.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", response.AccessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private static JwtSecurityToken GetToken(string jwtToken)
            => new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
    }
}