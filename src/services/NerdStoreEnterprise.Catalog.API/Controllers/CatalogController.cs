using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NerdStoreEnterprise.Catalog.API.Models;
using NerdStoreEnterprise.WebAPI.Core.Identity;
using System;
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
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();

            return Ok(products);
        }

        [ClaimsAuthorize("Catalog", "Read")]
        [HttpGet("catalog/products/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            var product = await _productRepository.GetAsync(id);

            return Ok(product);
        }
    }
}