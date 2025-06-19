using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Domain.Abstractions.Services;

public interface ICategoryService
{
    Task<long> CreateCategory(CategoryEntity categoryEntity);
    Task<(CategoryEntity, List<SubCategoryEntity>)?> GetSubCategoriesByCategory(long categoryId);
    Task<List<(CategoryEntity, List<SubCategoryEntity>)>> GetCategoriesWithSubCategories();
    Task<CategoryEntity?> GetCategory(long id);
    Task<List<CategoryEntity>> GetCategories();
    Task<long> UpdateCategory(long id, string name);
    Task<bool> DeleteCategory(long id);
    
}