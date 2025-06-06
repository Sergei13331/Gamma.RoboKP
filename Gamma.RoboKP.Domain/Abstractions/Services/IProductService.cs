using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Domain.Abstractions.Services;

public interface IProductService
{
    Task<long> CreateProduct(ProductEntity product);
}