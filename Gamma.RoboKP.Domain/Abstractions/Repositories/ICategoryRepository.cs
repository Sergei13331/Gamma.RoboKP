using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Domain.Abstractions.Repositories;

public interface ICategoryRepository
{
    Task<long> Create(CategoryEntity category);
    Task<CategoryEntity?> Get(long id);
    Task<CategoryEntity?> GetWithSubCategories(long categoryId);
    Task<List<CategoryEntity>> GetAll();
    Task<List<CategoryEntity>> GetAllWithSubCategories();
    Task<long> Update(long id, string name);
    Task<bool> Delete(long id);
}