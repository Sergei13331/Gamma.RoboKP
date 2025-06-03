using Gamma.RoboKP.Domain.Enums;

namespace Gamma.RoboKP.Models.User;

public record UserToRegister(
    string Name,
    string Surname,
    string LastName,
    UserRole Role,
    UserStatus Status,
    string Email,
    string Password
    );