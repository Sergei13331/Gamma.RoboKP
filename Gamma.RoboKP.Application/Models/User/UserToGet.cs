using Gamma.RoboKP.Domain.Enums;

namespace Gamma.RoboKP.Application.Models.User;

public record UserToGet(
    long Id,
    string Name,
    string SurName,
    string LastName,
    string Email,
    UserStatus Status,
    string Role,
    string Company
    );