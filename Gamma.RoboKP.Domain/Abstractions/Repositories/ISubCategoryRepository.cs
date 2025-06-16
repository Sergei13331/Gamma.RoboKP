using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Domain.Abstractions.Repositories;

public interface ISubCategoryRepository
{
    Task<(long, long)> CreateSubCategory(SubCategoryEntity subCategory);
    Task<SubCategoryEntity?> Get(long subCategoryId);
}