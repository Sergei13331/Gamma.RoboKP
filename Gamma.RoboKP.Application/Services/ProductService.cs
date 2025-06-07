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

    public async Task<ProductEntity?> GetProduct(long id)
    {
        return await productRepository.Get(id);
    }

    public async Task<List<ProductEntity>> GetProducts()
    {
        return await productRepository.GetAll();
    }

    public Task<long> UpdateProductData(long id, string name, string description, decimal price)
    {
        return productRepository.Update(id, name, description, price);
    }

    public Task<long> UpdateProductImage(long id, byte[] image)
    {
        return productRepository.UpdateImage(id, image);
    }

    public Task<bool> RemoveProduct(long id)
    {
        return productRepository.Delete(id);
    }
}