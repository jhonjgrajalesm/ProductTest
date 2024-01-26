using Microsoft.Extensions.Caching.Memory;
using PT.Domain.Entities;
using PT.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT.Services.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository productRespository;
        private IMemoryCache memoryCache;

        public ProductService(IProductRepository productRespository, IMemoryCache memoryCache)
        {
            this.productRespository = productRespository;
            this.memoryCache = memoryCache;
        }


        public async Task<Product> GetById(Guid productdId)
        {
            var memoryItem = (Product)this.memoryCache.Get(productdId);
            if (memoryItem == null)
            {
                var item = await this.productRespository.GetById(productdId);                
                if (item != null)
                {
                    SetItemMemory(item);
                }

                return item;
            }
            memoryItem.StatusName = Domain.Enums.StateType.Active;
            return memoryItem;
        }

        private void SetItemMemory(Product item)
        {
            MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            this.memoryCache.Set(item.ProductId, item, cacheEntryOptions);
        }

        public async Task<bool> Insert(Product product)
        {
            if (await this.productRespository.Insert(product))
            {
                CalculateFinalPrice(ref product);
                SetItemMemory(product);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CalculateFinalPrice(ref Product product)
        {
            product.FinalPrice = product.Price * (100 - product.Discount) / 100;
        }

        public async Task<bool> Update(Product product)
        {
            if (await this.productRespository.Update(product))
            {
                var memoryItem = this.memoryCache.Get(product.ProductId);
                if (memoryItem != null)
                {
                    memoryCache.Remove(product.ProductId);
                }
                CalculateFinalPrice(ref product);
                SetItemMemory(product);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
