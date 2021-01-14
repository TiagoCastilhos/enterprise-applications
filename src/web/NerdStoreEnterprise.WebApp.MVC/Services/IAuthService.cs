using NerdStoreEnterprise.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.WebApp.MVC.Services
{
    public interface IAuthService
    {
        Task<UserLoginResponse> Login(UserLoginViewModel userLoginViewModel);
        Task<UserLoginResponse> Register(RegisterUserViewModel registerUserViewModel);
    }
}