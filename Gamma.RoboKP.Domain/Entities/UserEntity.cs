// using Gamma.RoboKP.Domain.Enums;
// using Microsoft.AspNetCore.Identity;
//
// namespace Gamma.RoboKP.Domain.Entities;
//
// public class UserEntity : IdentityUser<long> //TODO: в другой слой, убрать зависимость
// {
//     //ctor
//     public string FirstName { get; set; } = string.Empty; //TODO: private set
//     public string Surname { get; set; } = string.Empty;
//     public string LastName { get; set; } = string.Empty;
//     public UserStatus Status { get; set; }
//     public string Company  { get; set; } = string.Empty; //TODO: объект 
//     
//     //TODO: добавить сюда поле refreshToken что бы изменять рефреш токен можно было только через него
//     
//     //TODO: убрать варнинги, create method
// }