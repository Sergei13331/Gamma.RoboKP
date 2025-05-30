using System.ComponentModel.DataAnnotations;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Domain.Enums;
using Gamma.RoboKP.Domain.ValueObject;
using Microsoft.AspNetCore.Identity;

namespace Gamma.RoboKP.Infrastructure.Identity;

public class AppUser : IdentityUser<long>
{
    [Required]
    [MaxLength(255)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(255)]
    public string SurName { get; set; } = string.Empty; 
    
    [Required]
    [MaxLength(255)]
    public string LastName { get; set; } = string.Empty; 
    
    public UserStatus Status { get; set; }
    
    public required Company Company  { get; set; }
}