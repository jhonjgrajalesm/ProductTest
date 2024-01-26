using PT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT.Services.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetById(Guid productId);
        Task<bool> Insert(Product product);
        Task<bool> Update(Product product);
    }
}
