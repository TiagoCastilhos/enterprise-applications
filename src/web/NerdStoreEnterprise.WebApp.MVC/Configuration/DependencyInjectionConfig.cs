using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.WebApp.MVC.Extensions;
using NerdStoreEnterprise.WebApp.MVC.Services;

namespace NerdStoreEnterprise.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthService, AuthenticationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            return services;
        }
    }
}