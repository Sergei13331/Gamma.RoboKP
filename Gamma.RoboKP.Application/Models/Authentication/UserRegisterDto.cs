using Gamma.RoboKP.Domain.Enums;
using Gamma.RoboKP.Domain.ValueObject;

namespace Gamma.RoboKP.Application.Models.Authentication;

public record UserRegisterDto(
    string FirstName,
    string SurName,
    string LastName,
    string Email,
    string Password,
    Company Company
    );