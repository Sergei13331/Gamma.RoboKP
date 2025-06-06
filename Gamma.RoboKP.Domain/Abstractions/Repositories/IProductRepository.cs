using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Domain.Abstractions.Repositories;

public interface IProductRepository
{
    Task<long> Create(ProductEntity product);
    Task<ProductEntity?> Get(long id);
    Task<List<ProductEntity>> GetAll();
    Task<long> Update(long id, string name, string description, decimal price);
    Task<long> UpdateImage(long id, byte[] image);
    Task<bool> Delete(long id);
}