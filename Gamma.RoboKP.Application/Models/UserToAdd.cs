using Gamma.RoboKP.Domain.Enums;

namespace Gamma.RoboKP.Application.Models;

public record UserToAdd(
    UserRole Role,
    UserStatus Status,
    string Email,
    string PasswordHash
    );