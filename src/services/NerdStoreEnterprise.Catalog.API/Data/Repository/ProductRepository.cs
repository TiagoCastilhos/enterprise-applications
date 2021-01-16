using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.Catalog.API.Models;
using NerdStoreEnterprise.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.Catalog.API.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public void Adicionar(Product produto)
        {
            _context.Products.Add(produto);
        }

        public void Atualizar(Product produto)
        {
            _context.Products.Update(produto);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}