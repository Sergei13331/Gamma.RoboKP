using Gamma.RoboKP.Application.Models;

namespace Gamma.RoboKP.Application.Abstractions.Services;

public interface IUserService
{
    Task Register(UserToRegister userToRegister);
}