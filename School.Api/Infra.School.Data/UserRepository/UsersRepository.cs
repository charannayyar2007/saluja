
using Infra.School.Data.Contract;
using Infra.School.Data.Db;
using School.Domain.Users;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.School.Data.UserRepository
{
    public class UsersRepository : IUserRepository
    {
        public Task ChangePassword(UserRegistertion user)
        {
            throw new NotImplementedException();
        }

        public Task ChangeRole(UserRegistertion user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserRegistertion> GetUser(string id)
        {

            using (var entity = new SchoolErp())
            {
                var user = await entity.Users.Where(x => x.userId == id || x.PhoneNo == id || x.EmailId == id).FirstOrDefaultAsync();
               // var user = await entity.Users.Where(x => x.userId == id).FirstOrDefaultAsync();

                if (user != null)
                {
                    var returnResult = new UserRegistertion()
                    {
                        PasswordSalt = user.PasswordSalt,
                        PasswordHash = user.PasswordHash,
                        userId = user.userId,
                        numberOfFiledLogin = user.numberOfFiledLogin,
                        isActive = user.isActive,
                        EmailId = user.EmailId,
                        PhoneNo = user.PhoneNo,
                        UniqueId = user.UniqueId,
                        roleId = entity.UserRoles.Where(x => x.UniqueId == user.UniqueId).FirstOrDefault().roleId
                    };
                    return returnResult;
                }
            }
           
               

                return null;
        }
        public async Task<UserRegistertion> GetStudentByAdmissionId(string id)
        {

            using (var entity = new SchoolErp())
            {
                var user =await (from us in entity.Users
                           join std in entity.Students
   on us.UniqueId equals std.UniqueId
                           where us.userId == id
                           select us).FirstOrDefaultAsync();
                if (user != null)
                {
                    var returnResult = new UserRegistertion()
                    {
                        PasswordSalt = user.PasswordSalt,
                        PasswordHash = user.PasswordHash,
                        userId = user.userId,
                        numberOfFiledLogin = user.numberOfFiledLogin,
                        isActive = user.isActive,
                        EmailId = user.EmailId,
                        PhoneNo = user.PhoneNo,
                        UniqueId = user.UniqueId,
                        roleId = entity.UserRoles.Where(x => x.UniqueId == user.UniqueId).FirstOrDefault().roleId
                    };
                    return returnResult;
                }
            }



            return null;
        }
        public async Task<Guid>  InsertUser(UserRegistertion user)
        {
                using (var entity = new SchoolErp())
                {

                User users = new User()
                {
                    userId = user.userId,
                    PasswordHash = user.PasswordHash,
                    PasswordSalt = user.PasswordSalt,
                    createOn = user.createOn,
                    lastLogin = user.lastLogin,
                    EmailId = user.EmailId,
                    PhoneNo = user.userId,
                    UniqueId=user.UniqueId
                    };
                    entity.Users.Add(users);
                    var result= await entity.SaveChangesAsync();
                    if(result>=1)
                    {
                        var userRole = new UserRole() { UniqueId = user.UniqueId, roleId = user.roleId };
                        entity.UserRoles.Add(userRole);
                        await entity.SaveChangesAsync();
                    return user.UniqueId;
                    }
                }
            return Guid.Empty;
        }
        public async Task<string> IsValidToken(string token,string userId)
        {
            using (var enitity = new SchoolErp())
            {
               var result=await enitity.PassRecoveries.Where(x=>x.token==token &&x.userid== userId && x.expiryDate>DateTime.Now).FirstOrDefaultAsync();
                if (result != null)
                    return result.userid;
            }
            return null;
        }

      

        public async Task<string> PasswordRecoveryToken(string userId,string callfrom)
        {
            string result = string.Empty;
            using (var enitity = new SchoolErp())
            {

               result = await Task.Run(()=>enitity.Proc_PasswordRecover(userId, callfrom).FirstOrDefault());
            }            
            return result;
        }

        public void Update(UserRegistertion updateObj)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateLoginlast(string id)
        {
            using (var enitity = new SchoolErp())
            {
                var user = await enitity.Users.Where(x => x.userId == id || x.PhoneNo == id || x.EmailId == id).FirstOrDefaultAsync();
                user.lastLogin = DateTime.Now;
               
               await enitity.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdatePassword(string Userid, byte[] PasswordHas, byte[] PasswordSlat)
        {
            using (var enitity = new SchoolErp())
            {
                var result = enitity.Users.Where(x => x.userId == Userid||x.PhoneNo==Userid ||x.EmailId==Userid).FirstOrDefault();
                result.PasswordHash = PasswordHas;
                result.PasswordSalt = PasswordSlat;
            var isupdate=    await enitity.SaveChangesAsync();
            if(isupdate > 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// USer Detail
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //public async Task<TeacherDetails> GetUserDetail(string userId)
        //{
        //    using (var context = new SchoolErp())
        //    {
        //        var results = await context.Where(x => x.Email == userId || x.Phone == userId).FirstOrDefaultAsync();
        //        if (results != null)
        //        {
        //            var result = new TeacherDetails()
        //            {
        //                FirstName = results.firstName,
        //                Lastname = results.Lastname,
        //                Address1 = results.Address1,
        //                Address2 = results.Address2,
        //                Dob = results.Dob,
        //                Doj = results.Doj,
        //                Email = results.Email,
        //                MidName = results.midName,
        //                EnrollmentCode = results.enrollmentCode,
        //                Phone = results.Phone

        //            };
        //            return result;
        //        }

        //        return null;
        //    }
        //}
    }
}
