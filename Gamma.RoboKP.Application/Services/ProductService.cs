using Gamma.RoboKP.Domain.Abstractions.Repositories;
using Gamma.RoboKP.Domain.Abstractions.Services;
using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<long> CreateProduct(ProductEntity product)
    {
        return await productRepository.Create(product);
    }
}