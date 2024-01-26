using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using PT.Domain.Entities;
using PT.Services.Interfaces;
using System.Transactions;

namespace PT.Services.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private IMemoryCache memoryCache;
        public const string Table = "ProdcutList";

        public ProductRepository(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        public async Task<Product> GetById(Guid productdId)
        {
            return this.GetAll().FirstOrDefault(u => u.ProductId == productdId);
        }

        public async Task<bool> Insert(Product product)
        {
            var products = this.GetAll();
            if (!products.Any(u => u.ProductId == product.ProductId))
            {
                products.Add(product);
                this.memoryCache.Set(Table, products);
                return true;
            }
            return false;
        }

        public async Task<bool> Update(Product product)
        {
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    var products = this.GetAll();
                    var productUpdate = this.GetAll().FirstOrDefault(u => u.ProductId == product.ProductId);
                    if (productUpdate != null)
                    {
                        products.Remove(productUpdate);
                        products.Add(product);
                    }
                    this.memoryCache.Set(Table, products);
                    tran.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal List<Product> GetAll()
        {
            return ((List<Product>)(this.memoryCache.Get(Table) ?? new List<Product>()));
        }
    }
}
