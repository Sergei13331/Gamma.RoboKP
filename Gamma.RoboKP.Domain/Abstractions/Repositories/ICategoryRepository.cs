using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Domain.Abstractions.Repositories;

public interface ICategoryRepository
{
    Task<long> Create(CategoryEntity category);
}