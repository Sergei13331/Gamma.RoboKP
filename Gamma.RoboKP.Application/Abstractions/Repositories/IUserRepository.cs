using Gamma.RoboKP.Domain.Entites;

namespace Gamma.RoboKP.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<Guid> Add(UserEntity user);
}