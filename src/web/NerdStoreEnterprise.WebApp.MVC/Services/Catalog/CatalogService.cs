using Microsoft.Extensions.Options;
using NerdStoreEnterprise.WebApp.MVC.Extensions;
using NerdStoreEnterprise.WebApp.MVC.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.WebApp.MVC.Services.Catalog
{
    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public CatalogService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _baseUri = settings.Value.CatalogUrl;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUri}/catalog/products");

            HandleResponseErrors(response);

            return await DeserializeAsync<IEnumerable<ProductViewModel>>(response);
        }

        public async Task<ProductViewModel> GetAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_baseUri}/catalog/products/{id}");

            HandleResponseErrors(response);

            return await DeserializeAsync<ProductViewModel>(response);
        }
    }
}