using School.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.School.Data.Contract
{
    public interface IUserRepository
    {
       Task<Guid> InsertUser(UserRegistertion user);
        Task<UserRegistertion> GetUser(string UserName);

        Task ChangePassword(UserRegistertion user);

        Task ChangeRole(UserRegistertion user);
        Task UpdateLoginlast(string userId);
        Task<string> PasswordRecoveryToken(string userId,string callfrom);

        Task<string> IsValidToken(string token,string userId);
        Task<UserRegistertion> GetStudentByAdmissionId(string id);
        Task<bool> UpdatePassword(string Userid, byte[] PasswordHas, byte[] PasswordSlat);
    }
}
