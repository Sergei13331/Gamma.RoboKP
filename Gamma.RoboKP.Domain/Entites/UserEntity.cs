using Gamma.RoboKP.Domain.Enums;

namespace Gamma.RoboKP.Domain.Entites;

public class UserEntity
{
    public UserEntity(Guid id, string firstName, string surname, string lastName ,UserRole role, UserStatus status, string email, string passwordHash)
    {
        Id = id;
        FirstName = firstName;
        Surname = surname;
        LastName = lastName;
        Role = role;
        Status = status;
        Email = email;
        PasswordHash = passwordHash;
    }
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string LastName { get; set; }
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public static UserEntity Create(Guid id, string firstname, string surname, string lastname ,UserRole role, UserStatus status, string email, string passwordHash)
    {
        return new UserEntity(id, firstname, surname, lastname, role, status, email, passwordHash);
    }
}