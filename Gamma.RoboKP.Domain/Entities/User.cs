using Gamma.RoboKP.Domain.Enums;
using Gamma.RoboKP.Domain.ValueObject;

namespace Gamma.RoboKP.Domain.Entities;

public class User : BaseEntity<long>
{
    
    public User() { }
    
    private User(string firstName, string surName, string lastName, UserStatus status, UserRole role, Company company, string email)
    {
        FirstName = firstName;
        SurName = surName;
        LastName = lastName;
        Status = status;
        Role = role;
        Company = company;
        Email = email;
        //EmailConfirmed = emailConfirmed;
       // PasswordHash = passwordHash;
    }
    
    public string FirstName { get; private set; } 
    public string SurName { get; private set; }
    public string LastName { get; private set; }
    public UserStatus Status { get; private set; }
    public UserRole Role { get; private set; }
    public Company Company { get; private set; }
    public string Email { get; private set; }
    public bool EmailConfirmed { get; private set; } = false;
    
    public string PasswordHash { get; private set; }

    public void ChangeEmail(Email email)
    {
        if (email == Email) return;
        Email = email;
        EmailConfirmed = false;
        
        //domain event для подтверждения
    }

    public void ChangeStatus(UserStatus status)
    {
        Status = status;
    }

    public static User Create(string firstName,
        string surName,
        string lastName,
        UserStatus status,
        UserRole role,
        Company company,
        string email)
    {
        return new User(firstName, surName, lastName, status, role, company, email);
    }
}