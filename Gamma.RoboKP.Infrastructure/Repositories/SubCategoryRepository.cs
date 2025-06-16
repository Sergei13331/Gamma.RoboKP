using Gamma.RoboKP.Domain.Abstractions.Repositories;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Infrastructure.Context;
using Gamma.RoboKP.Infrastructure.Models;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gamma.RoboKP.Infrastructure.Repositories;

public class SubCategoryRepository(
    [FromKeyedServices("RepositoryMapper")] IMapper mapper,
    RoboKpDbContext context) : ISubCategoryRepository
{
    public async Task<(long, long)> CreateSubCategory(SubCategoryEntity subCategory)
    {
        var subCategoryDb = mapper.Map<SubCategoryEntity, SubCategory>(subCategory);
        
        await context.AddAsync(subCategoryDb);
        await context.SaveChangesAsync();
        
        return (subCategoryDb.Id, subCategoryDb.ParentCategoryId);
    }

    public async Task<SubCategoryEntity?> Get(long subCategoryId)
    {
        var subCategory = await context.SubCategories.FindAsync(subCategoryId);
        
        return subCategory != null ? mapper.Map<SubCategory, SubCategoryEntity>(subCategory) : null;
    }
}