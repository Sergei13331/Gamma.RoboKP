using Gamma.RoboKP.Domain.Abstractions.Repositories;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Infrastructure.Context;
using MapsterMapper;
using Gamma.RoboKP.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gamma.RoboKP.Infrastructure.Repositories;

public class ProductRepository([FromKeyedServices("RepositoryMapper")] IMapper mapper, RoboKpDbContext context) : IProductRepository
{
    public async Task<long> Create(ProductEntity product)
    {
        var productDb = mapper.Map<ProductEntity, Product>(product);
        
        await context.Products.AddAsync(productDb);
        await context.SaveChangesAsync();
        
        return productDb.Id;
    }

    public async Task<ProductEntity?> Get(long id)
    {
        var productDb = await context.Products.FindAsync(id);
        return productDb != null ? mapper.Map<ProductEntity>(productDb) : null;
    }

    public async Task<List<ProductEntity>> GetAll()
    {
        var productsDb = await context.Products.ToListAsync();
        return mapper.Map<List<ProductEntity>>(productsDb);
    }

    public async Task<List<ProductEntity>?> SearchByName(string name)
    {
        var productDb = await context.Products.Where(p => p.Name.Contains(name)).ToListAsync();
        if (!productDb.Any()) return null;
        
        return mapper.Map<List<ProductEntity>>(productDb);
    }

    public async Task<List<ProductEntity>?> SearchByPrice(decimal price)
    {
        var productDb = await context.Products
            .AsNoTracking()
            .Where(p => p.Price == price)
            .OrderBy(p => p.Price)
            .ToListAsync();
        
        if (!productDb.Any()) return null;
        
        return mapper.Map<List<ProductEntity>>(productDb);
    }
    
    public async Task<List<ProductEntity>?> SearchByCategory(long? categoryId = null, long? subCategoryId = null)
    {
        if (categoryId != null)
        {
            var category = await context.Categories.FindAsync(categoryId);
            if (category == null) return null;
        }

        if (subCategoryId != null)
        {
            var subCategory = await context.SubCategories.FindAsync(subCategoryId);
            if (subCategory == null) return null;
        }

        var query = context.Products.AsNoTracking().AsQueryable();
        
        if (categoryId != null)
        {

            query = context.Products.Where(p => p.CategoryId == categoryId);
        }

        if (subCategoryId.HasValue)
        {
            query = query.Where(p => p.SubCategoryId == subCategoryId);
        }
        
        var products = await query.ToListAsync();
        
        return products.Any()
            ? mapper.Map<List<ProductEntity>>(products) 
            : null;
    }
    
    public async Task<List<ProductEntity>?> SearchProducts(
        string? name = null,
        decimal? exactPrice = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        long? categoryId = null,
        long? subCategoryId = null,
        int pageNumber = 1,
        int pageSize = 50)
    {
        var query = context.Products.AsNoTracking().AsQueryable();

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId);
            
        }
        
        if (subCategoryId.HasValue)
        {
            query = query.Where(p => p.SubCategoryId == subCategoryId);
        }
        
        if (exactPrice.HasValue)
        {
            query = query.Where(p => p.Price == exactPrice.Value);
        }
        else
        {
            if (minPrice.HasValue && maxPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value && p.Price <= maxPrice.Value);
            }
            else if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }
            else if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }
        }
        
        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(p => p.Name.Contains(name));
        }
        
        query = query.OrderBy(p => p.Price)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        var productDb = await query.ToListAsync();
    
        return productDb.Any() 
            ? mapper.Map<List<ProductEntity>>(productDb) 
            : null;
    }
    
    public async Task<long> Update(long id, string name, string description, decimal price, SubCategoryEntity subCategory)
    {
        var productDb = await context.Products.FindAsync(id);
        if (productDb == null) return 0;
        
        if (productDb.Name != name) productDb.Name = name;
        if (productDb.Description != description) productDb.Description = description;
        if (productDb.Price != price) productDb.Price = price;
        if (productDb.CategoryId != subCategory.ParentCategoryId) productDb.CategoryId = subCategory.ParentCategoryId;
        if (productDb.SubCategoryId != subCategory.Id) productDb.CategoryId = subCategory.ParentCategoryId;
        
        
        context.Products.Update(productDb);
        return await context.SaveChangesAsync() > 0 ? id : 0;
    }

    public async Task<long> UpdateImage(long id, string imageUrl)
    {
        var productDb = await context.Products.FindAsync(id);
        if (productDb == null) return 0;
        productDb.ImageUrl = imageUrl;
        context.Products.Update(productDb);
        return await context.SaveChangesAsync() > 0 ? id : 0;
    }

    public async Task<bool> Delete(long id)
    {
        var productDb = await context.Products.FindAsync(id);
        if (productDb == null) return false;
        context.Products.Remove(productDb);
        return await context.SaveChangesAsync() > 0;
    }
}