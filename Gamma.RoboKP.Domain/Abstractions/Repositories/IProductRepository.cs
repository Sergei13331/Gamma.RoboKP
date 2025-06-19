using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Domain.Abstractions.Repositories;

public interface IProductRepository
{
    Task<long> Create(ProductEntity product);
    Task<ProductEntity?> Get(long id);
    Task<List<ProductEntity>> GetAll();
    Task<List<ProductEntity>?> SearchByName(string name);
    Task<List<ProductEntity>?> SearchByPrice(decimal price);
    Task<List<ProductEntity>?> SearchByCategory(long categoryId, long? subCategoryId = null);
    Task<List<ProductEntity>?> SearchProducts(string? name = null, decimal? exactPrice = null, decimal? minPrice = null,
        decimal? maxPrice = null, long? categoryId = null, long? subCategoryId = null, int pageNumber = 1, int pageSize = 50);
    Task<long> Update(long id, string name, string description, decimal price, SubCategoryEntity subCategory);
    Task<long> UpdateImage(long id, string imageUrl);
    Task<bool> Delete(long id);
}