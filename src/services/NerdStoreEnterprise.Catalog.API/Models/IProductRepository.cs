using NerdStoreEnterprise.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.Catalog.API.Models
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetAsync(Guid id);

        void Adicionar(Product produto);
        void Atualizar(Product produto);
    }
}