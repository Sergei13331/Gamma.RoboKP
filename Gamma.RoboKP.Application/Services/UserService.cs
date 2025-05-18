using Gamma.RoboKP.Application.Abstractions.Auth;
using Gamma.RoboKP.Application.Abstractions.Repositories;
using Gamma.RoboKP.Application.Abstractions.Services;
using Gamma.RoboKP.Application.Models;
using Gamma.RoboKP.Domain.Entites;
using MapsterMapper;

namespace Gamma.RoboKP.Application.Services;

public class UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher) : IUserService
{
    public async Task Register(UserToRegister userToRegister)
    {
        var hashedPassword = passwordHasher.Generate(userToRegister.Password);
        
        var user = UserEntity.Create(
            Guid.NewGuid(), 
            userToRegister.Name,
            userToRegister.Surname,
            userToRegister.LastName,
            userToRegister.Role,
            userToRegister.Status,
            userToRegister.Email,
            hashedPassword);
        
        await userRepository.Add(user);
    }
    
    
}