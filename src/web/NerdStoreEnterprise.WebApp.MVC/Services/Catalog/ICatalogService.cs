using NerdStoreEnterprise.WebApp.MVC.Models.Catalog;
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
}