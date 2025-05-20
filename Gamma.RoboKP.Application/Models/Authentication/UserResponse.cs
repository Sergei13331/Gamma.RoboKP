using Gamma.RoboKP.Domain.Enums;

namespace Gamma.RoboKP.Application.Models.Authentication;

public class UserResponse{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public UserStatus Status { get; set; }
    public string Email {get; set;}
    
    public string Company { get; set; }
    public string UserName { get; set; }
    public string Token {get; set;}
    };