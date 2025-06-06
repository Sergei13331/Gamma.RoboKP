using Gamma.RoboKP.Domain.Abstractions.Repositories;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Infrastructure.Context;
using MapsterMapper;
using Gamma.RoboKP.Infrastructure.Models;

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

    public Task<ProductEntity> Get(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductEntity>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<long> Update(long id, string name, string description, decimal price)
    {
        throw new NotImplementedException();
    }

    public Task<long> UpdateImage(long id, byte[] image)
    {
        throw new NotImplementedException();
    }

    public Task Delete(long id)
    {
        throw new NotImplementedException();
    }
}
