﻿using Microsoft.AspNetCore.Mvc;
using NerdStoreEnterprise.WebApp.MVC.Services.Catalog;
using System;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.WebApp.MVC.Controllers
{
    public class CatalogController : MainController
    {
        private readonly ICatalogServiceRefit _catalogService;

        public CatalogController(ICatalogServiceRefit catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("")]
        [Route("products")]
        public async Task<IActionResult> Index()
        {
            var products = await _catalogService.GetAsync();

            return View(products);
        }

        [HttpGet]
        [Route("product-details/{id}")]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            var product = await _catalogService.GetAsync(id);

            return View(product);
        }
    }
}