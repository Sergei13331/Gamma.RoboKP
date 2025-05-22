using Gamma.RoboKP.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Gamma.RoboKP.Domain.Entities;

public class UserEntity : IdentityUser<long>
{ 
    public string FirstName { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserStatus Status { get; set; }
    public string Company  { get; set; } = string.Empty;
}