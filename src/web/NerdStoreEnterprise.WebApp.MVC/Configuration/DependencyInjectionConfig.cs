using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.WebApp.MVC.Services;

namespace NerdStoreEnterprise.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}