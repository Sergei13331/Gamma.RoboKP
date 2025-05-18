using Gamma.RoboKP.Application.Abstractions.Repositories;
using Gamma.RoboKP.Domain.Entites;
using Gamma.RoboKP.Infrastructure.Context;

namespace Gamma.RoboKP.Infrastructure.Repositories;

public class UserRepository(RoboKpDbContext context) : IUserRepository
{
    public async Task<Guid> Add(UserEntity user)
    {
        var newEntity = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        
        return newEntity.Entity.Id;
    }
}