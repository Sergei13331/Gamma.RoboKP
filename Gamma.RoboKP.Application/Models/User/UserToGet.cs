using Gamma.RoboKP.Domain.Enums;
using Gamma.RoboKP.Domain.ValueObject;

namespace Gamma.RoboKP.Application.Models.User;

public record UserToGet(
    long Id,
    string FirstName,
    string SurName,
    string LastName,
    string Email,
    UserStatus Status,
    string Role,
    Company Company
    );