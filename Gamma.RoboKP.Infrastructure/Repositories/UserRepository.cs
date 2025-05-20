using Gamma.RoboKP.Application.Abstractions.Repositories;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Gamma.RoboKP.Infrastructure.Repositories;

public class UserRepository(RoboKpDbContext context) : IUserRepository
{
    public async Task<Guid> Add(UserEntity user)
    {
        var newEntity = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        
        return newEntity.Entity.Id;
    }

    public async Task<UserEntity> GetByEmail(string email)
    {
        var userEntity = context.Users
            .AsNoTracking()
            .FirstOrDefault(u => u.Email == email) ?? throw new Exception("User not found");
        
        return userEntity;
    }
}