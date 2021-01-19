using NerdStoreEnterprise.WebApp.MVC.Models.Identity;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.WebApp.MVC.Services.Identity
{
    public interface IAuthService
    {
        Task<UserLoginResponse> Login(UserLoginViewModel userLoginViewModel);
        Task<UserLoginResponse> Register(RegisterUserViewModel registerUserViewModel);
    }
}