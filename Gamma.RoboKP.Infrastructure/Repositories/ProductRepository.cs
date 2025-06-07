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

    public async Task<long> Update(long id, string name, string description, decimal price)
    {
        var productDb = await context.Products.FindAsync(id);
        if (productDb == null) return 0;
        productDb.Name = name;
        productDb.Description = description;
        productDb.Price = price;
        context.Products.Update(productDb);
        return await context.SaveChangesAsync() > 0 ? id : 0;
    }

    public Task<long> UpdateImage(long id, byte[] image)
    {
        throw new NotImplementedException(); // TODO Перевод в формат URI
    }

    public async Task<bool> Delete(long id)
    {
        var productDb = await context.Products.FindAsync(id);
        if (productDb == null) return false;
        context.Products.Remove(productDb);
        return await context.SaveChangesAsync() > 0;
    }
}
