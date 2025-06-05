using System.ComponentModel.DataAnnotations;

namespace Gamma.RoboKP.Models.Authentication;

public record UserLoginDto(
    [Required(ErrorMessage = "Поле 'почта' обязательно для заполнения")]
    [EmailAddress(ErrorMessage = "Неверный формат почты")]
    string Email,
    
    [Required(ErrorMessage = "Поле 'пароль' обязательно для заполнения")]
    string Password
    );
