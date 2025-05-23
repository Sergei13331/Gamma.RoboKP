using Gamma.RoboKP.Domain.Enums;

namespace Gamma.RoboKP.Application.Models.Authentication;

public record UserRegisterDto(
    string Name,
    string Surname,
    string LastName,
    string Email,
    string Password,
    string Company
    );