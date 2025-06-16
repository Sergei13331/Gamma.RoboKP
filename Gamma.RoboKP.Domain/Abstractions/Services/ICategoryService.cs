using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Domain.Abstractions.Services;

public interface ICategoryService
{
    Task<long> CreateCategory(CategoryEntity categoryEntity);
}