using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Domain.ValueObject;
using Microsoft.AspNetCore.Identity;

namespace Gamma.RoboKP.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User?> FindByEmailAsync(string email);
    Task<bool> AddAsync(User user, string password);
    Task<IdentityResult> AddToRole(User user, string role);
    Task<bool> CheckPassword(User user, string password);
    Task<string?> GetRole(User user);
}