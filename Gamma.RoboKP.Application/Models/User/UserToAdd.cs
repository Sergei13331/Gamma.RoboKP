using Gamma.RoboKP.Domain.Enums;

namespace Gamma.RoboKP.Application.Models.User;

public record UserToAdd(
    UserRole Role,
    UserStatus Status,
    string Email,
    string PasswordHash
    );