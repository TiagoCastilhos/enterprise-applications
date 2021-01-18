using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.Catalog.API.Data;
using NerdStoreEnterprise.Catalog.API.Data.Repository;
using NerdStoreEnterprise.Catalog.API.Models;

namespace NerdStoreEnterprise.Catalog.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<CatalogContext>();
        }
    }
}