using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.Catalog.API.Data;

namespace NerdStoreEnterprise.Catalog.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<CatalogContext>();
        }
    }
}