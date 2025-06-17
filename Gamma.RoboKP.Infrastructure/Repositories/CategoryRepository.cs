using Gamma.RoboKP.Domain.Abstractions.Repositories;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Infrastructure.Context;
using Gamma.RoboKP.Infrastructure.Models;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
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

    // TODO Этот функционал не работает! При переводе значений в список кортежей, результат пустой
    public async Task<List<(CategoryEntity, List<SubCategoryEntity>)>> GetAllWithSubCategories()
    {
        var categoriesDb = await context.Categories.ToListAsync();
        var categories = mapper.Map<List<CategoryEntity>>(categoriesDb);
        var result = categories.Select(
            cat => (
                cat,
                mapper.Map<List<SubCategoryEntity>>(
                context.SubCategories.Where(sc => sc.ParentCategoryId == cat.Id).ToListAsync()
                )
            )).ToList();
        return result;
    }
    
    public async Task<(CategoryEntity, List<SubCategoryEntity>)?> GetWithSubCategories(long categoryId)
    {
        var category = await context.Categories.FindAsync(categoryId);
        if (category == null) return null;
        var subCategories = await (context.SubCategories.Where(sc => sc.ParentCategoryId == categoryId
        ).ToListAsync());
        return (mapper.Map<CategoryEntity>(category), mapper.Map<List<SubCategoryEntity>>(subCategories));
    }

    public async Task<CategoryEntity?> Get(long id)
    {
        var category = await context.Categories.FindAsync(id);
        return category != null ? mapper.Map<CategoryEntity>(category) : null;
    }

    public async Task<List<CategoryEntity>> GetAll()
    {
        var categories = await context.Categories.ToListAsync();
        return mapper.Map<List<CategoryEntity>>(categories);
    }

    public async Task<long> Update(long id, string name)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null) return 0;
        category.Name = name;
        context.Categories.Update(category);
        return await context.SaveChangesAsync() > 0 ? id : 0;
    }

    public async Task<bool> Delete(long id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null) return false;
        context.Categories.Remove(category);
        return await context.SaveChangesAsync() > 0;
    }
}