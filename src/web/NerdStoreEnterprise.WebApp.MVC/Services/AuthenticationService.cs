using NerdStoreEnterprise.WebApp.MVC.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.WebApp.MVC.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserLoginResponse> Login(UserLoginViewModel userLoginViewModel)
        {
            var loginContent = new StringContent(JsonSerializer.Serialize(userLoginViewModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:44396/api/identity/login", loginContent);

            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<UserLoginResponse> Register(RegisterUserViewModel registerUserViewModel)
        {
            var registerContent = new StringContent(JsonSerializer.Serialize(registerUserViewModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:44396/api/identity/register", registerContent);

            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), options);
        }
    }
}