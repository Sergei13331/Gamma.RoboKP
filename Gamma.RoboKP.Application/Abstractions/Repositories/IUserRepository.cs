using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<Guid> Add(UserEntity user);
    Task<UserEntity> GetByEmail(string email);
}