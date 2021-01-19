using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.WebApp.MVC.Extensions;
using NerdStoreEnterprise.WebApp.MVC.Services.Catalog;
using NerdStoreEnterprise.WebApp.MVC.Services.Handlers;
using NerdStoreEnterprise.WebApp.MVC.Services.Identity;

namespace NerdStoreEnterprise.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthService, AuthenticationService>();

            services.AddHttpClient<ICatalogService, CatalogService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            return services;
        }
    }
}