using Microsoft.Extensions.Options;
using NerdStoreEnterprise.WebApp.MVC.Extensions;
using NerdStoreEnterprise.WebApp.MVC.Models;
using NerdStoreEnterprise.WebApp.MVC.Models.Identity;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.WebApp.MVC.Services.Identity
{
    public class AuthenticationService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public AuthenticationService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _baseUri = settings.Value.AuthenticationUrl;
        }

        public async Task<UserLoginResponse> Login(UserLoginViewModel userLoginViewModel)
        {
            var loginContent = new StringContent(JsonSerializer.Serialize(userLoginViewModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUri}/api/identity/login", loginContent);

            if (!HandleResponseErrors(response))
                return new UserLoginResponse(await DeserializeAsync<ResponseResult>(response));

            return await DeserializeAsync<UserLoginResponse>(response);
        }

        public async Task<UserLoginResponse> Register(RegisterUserViewModel registerUserViewModel)
        {
            var registerContent = new StringContent(JsonSerializer.Serialize(registerUserViewModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUri}/api/identity/register", registerContent);

            if (!HandleResponseErrors(response))
                return new UserLoginResponse(await DeserializeAsync<ResponseResult>(response));

            return await DeserializeAsync<UserLoginResponse>(response);
        }
    }
}