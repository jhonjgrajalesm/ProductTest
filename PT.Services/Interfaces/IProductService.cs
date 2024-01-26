using PT.Domain.Entities;

namespace PT.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetById(Guid productdId);
        Task<bool> Insert(Product product);
        Task<bool> Update(Product product);
    }
}