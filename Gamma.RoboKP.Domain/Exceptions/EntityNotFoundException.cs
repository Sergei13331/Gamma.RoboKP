using Microsoft.AspNetCore.Identity;

namespace Gamma.RoboKP.Domain.Exceptions;

public class EntityNotFoundException(IEnumerable<IdentityError> errors) : Exception
{
    //public UserEntity User { get; } = user;
    public IEnumerable<IdentityError> Errors { get; } = errors;
}