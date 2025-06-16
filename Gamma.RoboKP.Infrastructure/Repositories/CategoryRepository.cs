using Gamma.RoboKP.Domain.Abstractions.Repositories;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Infrastructure.Context;
using Gamma.RoboKP.Infrastructure.Models;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Gamma.RoboKP.Infrastructure.Repositories;

public class CategoryRepository(
    [FromKeyedServices("RepositoryMapper")] IMapper mapper,
    RoboKpDbContext context) : ICategoryRepository
{
    public async Task<long> Create(CategoryEntity category)
    {
        var productDb = mapper.Map<CategoryEntity, Category>(category);
        
        await context.Categories.AddAsync(productDb);
        await context.SaveChangesAsync();
        
        return productDb.Id;
    }
}