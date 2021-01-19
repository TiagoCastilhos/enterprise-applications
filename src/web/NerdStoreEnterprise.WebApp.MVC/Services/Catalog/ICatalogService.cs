using NerdStoreEnterprise.WebApp.MVC.Models.Catalog;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.WebApp.MVC.Services.Catalog
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductViewModel>> GetAsync();
        Task<ProductViewModel> GetAsync(Guid id);
    }

    public interface ICatalogServiceRefit
    {
        [Get("/catalog/products/")]
        Task<IEnumerable<ProductViewModel>> GetAsync();

        [Get("/catalog/products/{id}")]
        Task<ProductViewModel> GetAsync(Guid id);
    }
}