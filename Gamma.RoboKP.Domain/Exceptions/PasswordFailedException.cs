using Microsoft.AspNetCore.Identity;

namespace Gamma.RoboKP.Domain.Exceptions;

public class PasswordFailedException(IEnumerable<IdentityError> errors ) : Exception  
{
    public IEnumerable<IdentityError> Errors { get; } = errors;
}