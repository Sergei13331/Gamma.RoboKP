using Gamma.RoboKP.Domain.Abstractions.Repositories;
using Gamma.RoboKP.Domain.Abstractions.Services;
using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Application.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<long> CreateCategory(CategoryEntity categoryEntity)
    {
        return await categoryRepository.Create(categoryEntity);
    }
    
    public async Task<CategoryEntity?> GetSubCategoriesByCategory(long categoryId)
    {
        return await categoryRepository.GetWithSubCategories(categoryId);
    }

    public async Task<List<CategoryEntity>> GetCategoriesWithSubCategories()
    {
        return await categoryRepository.GetAllWithSubCategories();
    }

    public async Task<CategoryEntity?> GetCategory(long id)
    {
        return await categoryRepository.Get(id);
    }

    public async Task<List<CategoryEntity>> GetCategories()
    {
        return await categoryRepository.GetAll();
    }

    public async Task<long> UpdateCategory(long id, string name)
    {
        return await categoryRepository.Update(id, name);
    }

    public async Task<bool> DeleteCategory(long id)
    {
        return await categoryRepository.Delete(id);
    }
}