using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Application.Abstractions.Repositories;

public interface IRefreshTokeRepository
{
    Task<RefreshTokenEntity> Add(RefreshTokenEntity refreshToken);
}