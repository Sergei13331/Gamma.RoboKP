using Gamma.RoboKP.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Gamma.RoboKP.Domain.Exceptions;

public class NotValidUserException(User user, IEnumerable<IdentityError> errors) : Exception
{
    public User User { get; } = user;
    public IEnumerable<IdentityError> Errors { get; } = errors;
}