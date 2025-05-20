using Gamma.RoboKP.Domain.Enums;

namespace Gamma.RoboKP.Application.Models.Authentication;

public record UserRegisterDto(
    string Name,
    string Surname,
    string LastName,
    UserStatus Status,
    string Email,
    string Password,
    string Company
    );