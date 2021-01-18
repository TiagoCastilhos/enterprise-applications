using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NerdStoreEnterprise.Catalog.API.Models;
using NerdStoreEnterprise.WebAPI.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.Catalog.API.Controllers
{
    [ApiController]
    [Authorize]
    public class CatalogController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [AllowAnonymous]
        [HttpGet("catalog/products")]
        public async Task<IEnumerable<Product>> Index()
        {
            return await _productRepository.GetAllAsync();
        }

        [ClaimsAuthorize("Catalog", "Read")]
        [HttpGet("catalog/products/{id}")]
        public async Task<Product> ProdutoDetalhe(Guid id)
        {
            return await _productRepository.GetAsync(id);
        }
    }
}