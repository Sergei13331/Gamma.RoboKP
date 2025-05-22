// using Gamma.RoboKP.Application.Abstractions.Repositories;
// using Gamma.RoboKP.Domain.Entities;
// using Gamma.RoboKP.Infrastructure.Context;
// using Microsoft.EntityFrameworkCore;
//
// namespace Gamma.RoboKP.Infrastructure.Repositories;
//
// public class UsersRepository(RoboKpDbContext context) : IUserRepository
// {
//     public async Task<List<UserEntity>> GetAll()
//     {
//         return await context.Set<UserEntity>().ToListAsync();
//     }
//
//     public async Task<UserEntity?> GetById(long id)
//     {
//         var user = await context.Set<UserEntity>().FirstOrDefaultAsync(u => u.Id == id);
//         if (user is null) return null;
//         
//         return user;
//     }
//
//     public async Task<UserEntity?> GetByEmail(string email)
//     {
//         var user = await context.Set<UserEntity>().FirstOrDefaultAsync(u => u.Email == email);
//         if (user is null) return null;
//         return user;
//     }
//
//     public async Task<UserEntity> Update(UserEntity user)
//     {
//         var newUser = await context.Set<UserEntity>().ExecuteUpdateAsync(user);
//     }
//
//     public Task<bool> Delete(long id)
//     {
//         throw new NotImplementedException();
//     }
// }