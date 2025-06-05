using System.ComponentModel.DataAnnotations;

namespace Gamma.RoboKP.Models.Authentication;

public class ForgotPasswordDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    
    [Required]
    public string? ClientUri  { get; set; }
}