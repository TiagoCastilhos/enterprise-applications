using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NerdStoreEnterprise.Identity.API.Models;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.Identity.API.Controllers
{
    [Route("api/Identity")]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserViewModel registerUserViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = new IdentityUser
            {
                UserName = registerUserViewModel.Email,
                Email = registerUserViewModel.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUserViewModel.Senha);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginViewModel userLoginViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(userLoginViewModel.Email, userLoginViewModel.Senha, false, true);

            if (result.Succeeded)
                return Ok();

            return BadRequest();
        }
    }
}