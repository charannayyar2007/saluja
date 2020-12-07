using School.Domain.Users;
using School.Dto.Users;
using System;
using System.Threading.Tasks;

namespace School.Application.Contract
{
   public interface IUserService
    {
        Task<Guid> UserRegistration(UserRegistrationDto user);
        Task<bool> UserExists(string username);
        Task<UserRegistertion> AdmissionIdExists(string username);
        Task<UserLoginResponseForDto> Login(UserLoginDto user);
       Task<bool> PasswordRecoveryTokenGenerate(string userId);
        Task<UserDto> GetUserDetail(string userId);
        Task<string> IsValidToken(string token,string userId);
        Task<bool> UpdatePassword(UserPasswordResetForDto userPasswordResetForDto);
        Task<string> PasswordRecoveryTokenGenerateApp(string userId);
    }
}
