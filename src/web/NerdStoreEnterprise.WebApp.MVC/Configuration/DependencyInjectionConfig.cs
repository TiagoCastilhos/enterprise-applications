using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.WebApp.MVC.Extensions;
using NerdStoreEnterprise.WebApp.MVC.Services.Catalog;
using NerdStoreEnterprise.WebApp.MVC.Services.Handlers;
using NerdStoreEnterprise.WebApp.MVC.Services.Identity;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Net.Http;

namespace NerdStoreEnterprise.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthService, AuthenticationService>();

            services.AddHttpClient<ICatalogService, CatalogService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                //.AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));
                .AddPolicyHandler(PollyExtensions.WaitAndRetry())
                .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            //services
            //    .AddHttpClient("Refit", o =>
            //    {
            //        o.BaseAddress = new Uri(configuration.GetSection("CatalogUrl").Value);
            //    })
            //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //    .AddTypedClient(Refit.RestService.For<ICatalogServiceRefit>);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            return services;
        }
    }

    public class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> WaitAndRetry()
        {
            var retry = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                }, (outcome, timespan, retryCount, context) =>
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Tentando pela {retryCount} vez");
                    Console.ForegroundColor = ConsoleColor.White;
                });

            return retry;
        }
    }
}