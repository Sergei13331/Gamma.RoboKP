using Gamma.RoboKP.Domain.Abstractions.Repositories;
using Gamma.RoboKP.Domain.Abstractions.Services;
using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Application.Services;

public class SubCategoryService(ISubCategoryRepository repository) : ISubCategoryService
{
    public async Task<(long, long)> CreateCategory(SubCategoryEntity subCategoryEntity)
    {
        return await repository.CreateSubCategory(subCategoryEntity);
    }

    public async Task<SubCategoryEntity?> GetSubCategory(long id)
    {
        return await repository.Get(id);
    }
}