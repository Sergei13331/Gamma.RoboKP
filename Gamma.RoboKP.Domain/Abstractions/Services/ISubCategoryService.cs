using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Domain.Abstractions.Services;

public interface ISubCategoryService
{
    Task<(long, long)> CreateCategory(SubCategoryEntity subCategoryEntity);
    Task<SubCategoryEntity?> GetSubCategory(long id);
}