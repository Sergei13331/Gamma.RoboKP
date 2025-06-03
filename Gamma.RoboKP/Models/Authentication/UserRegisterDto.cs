using Gamma.RoboKP.Domain.ValueObject;

namespace Gamma.RoboKP.Models.Authentication;

public record UserRegisterDto(
    string FirstName,
    string SurName,
    string LastName,
    string Email,
    string Password,
    Company Company
    );