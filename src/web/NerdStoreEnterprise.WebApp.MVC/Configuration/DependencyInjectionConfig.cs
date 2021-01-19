using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.WebApp.MVC.Extensions;
using NerdStoreEnterprise.WebApp.MVC.Services.Catalog;
using NerdStoreEnterprise.WebApp.MVC.Services.Handlers;
using NerdStoreEnterprise.WebApp.MVC.Services.Identity;
using System;

namespace NerdStoreEnterprise.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthService, AuthenticationService>();

            //services.AddHttpClient<ICatalogService, CatalogService>()
            //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services
                .AddHttpClient("Refit", o =>
                {
                    o.BaseAddress = new Uri(configuration.GetSection("CatalogUrl").Value);
                })
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddTypedClient(Refit.RestService.For<ICatalogServiceRefit>);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            return services;
        }
    }
}