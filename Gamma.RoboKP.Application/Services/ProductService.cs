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

    public async Task<List<ProductEntity>?> GetProductsByName(string name)
    {
        return await productRepository.SearchByName(name);
    }

    public async Task<List<ProductEntity>?> GetProductByPrice(decimal price)
    {
        return await productRepository.SearchByPrice(price);
    }

    public async Task<List<ProductEntity>?> SearchAndFilter(
        string? name = null,
        decimal? exactPrice = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        int page = 1,
        int pageSize = 50
        )
    {
        return await productRepository.SearchProducts(name, exactPrice, minPrice, maxPrice);
    }

    public Task<long> UpdateProductData(long id, string name, string description, decimal price)
    {
        return productRepository.Update(id, name, description, price);
    }

    public Task<long> UpdateProductImage(long id, string imageUrl)
    {
        return productRepository.UpdateImage(id, imageUrl);
    }

    public Task<bool> RemoveProduct(long id)
    {
        return productRepository.Delete(id);
    }
}