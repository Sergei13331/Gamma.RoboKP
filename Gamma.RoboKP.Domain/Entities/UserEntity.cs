using Gamma.RoboKP.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Gamma.RoboKP.Domain.Entities;

public class UserEntity : IdentityUser<long>
{
    // public UserEntity(Guid id,
    //     string firstName,
    //     string surname,
    //     string lastName,
    //     UserRole role,
    //     UserStatus status,
    //     string email,
    //     string passwordHash,
    //     string company
    //     )
    // {
    //     Id = id;
    //     FirstName = firstName;
    //     Surname = surname;
    //     LastName = lastName;
    //     Role = role;
    //     Status = status;
    //     Email = email;
    //     PasswordHash = passwordHash;
    //     Company = company;
    // }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string LastName { get; set; }
    //public UserRole Role { get; set; }
    public UserStatus Status { get; set; }
    public string Company  { get; set; }
    // public static UserEntity Create(Guid id,
    //     string firstname,
    //     string surname,
    //     string lastname,
    //     UserRole role,
    //     UserStatus status,
    //     string email,
    //     string passwordHash,
    //     string company)
    // {
    //     return new UserEntity(id, firstname, surname, lastname, role, status, email, passwordHash, company);
    // }
}