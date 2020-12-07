using Infra.School.Data.Contract;
using Infra.School.Data.DataFactory;
using Infra.School.Data.UserRepository;
using MailAndSmsService.Sms;
using School.Application.Contract;
using School.Domain.Users;
using School.Dto.Users;
using School.Utility.EmailService;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace School.Application.UsersService
{
   public class UserRegistrationService: BaseService, IUserService
    {
       private readonly IUserRepository _userRepository;
        public UserRegistrationService()
        {
            _userRepository = FactoryDataLayer<UsersRepository>.Create();
        }
        //public void InsertUser(UserRegistrationDto user)
        //{
 
        //   var mapToUser= mapper.Map<UserRegistertion>(user);
        //    _userRepository.InsertUser(mapToUser);
        //}
        public async Task<Guid> UserRegistration(UserRegistrationDto user)
        {

             byte[] passwordHash, passwordSalt;
                CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
                var userToCreate = mapper.Map<UserRegistertion>(user);
                userToCreate.PasswordHash = passwordHash;
                userToCreate.PasswordSalt = passwordSalt;
              
          return await _userRepository.InsertUser(userToCreate);
           
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<UserLoginResponseForDto> Login(UserLoginDto user)
        {
            var userdata =await _userRepository.GetUser(user.UserId);
           
            if (userdata == null)
                return null;

            if (!VerifyPasswordHash(user.Password, userdata.PasswordHash, userdata.PasswordSalt))
            {
                return null;
            }
          await  updateLastLogin(user.UserId);
           var userResponse= mapper.Map<UserLoginResponseForDto>(userdata);
            return userResponse;
        }
        private async Task updateLastLogin(string id)
        {
          await  _userRepository.UpdateLoginlast(id);
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }
        public async Task<bool> UserExists(string username)
        {
            var userdata = await _userRepository.GetUser(username);
            if (userdata!=null)
                return true;

            return false;
        }

        public async Task<bool> PasswordRecoveryTokenGenerate(string userId)
        {
            string token =await _userRepository.PasswordRecoveryToken(userId,"web");
           string issuer = ConfigurationManager.AppSettings["issuer"];
           string url = issuer + "/" + "restpassword?token="+token;
         if (EmailSendService.SendMail("<h1>Password Recovery</h1>" + "<a href=" + url + "></a>", userId, "Password recovery"))
            {
                return true;
            }
            return false;
        }

        public async Task<string> IsValidToken(string token,string userId)
        {
            return await _userRepository.IsValidToken(token,userId);
        }

        public async Task<bool> UpdatePassword(UserPasswordResetForDto userPasswordReset)
        {
         
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userPasswordReset.Password, out passwordHash, out passwordSalt);
            
            return await _userRepository.UpdatePassword(userPasswordReset.UserId,passwordHash,passwordSalt); 
        }

        public Task<UserDto> GetUserDetail(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> PasswordRecoveryTokenGenerateApp(string userId)
        {
           var result= await _userRepository.GetUser(userId);
            var otp= await _userRepository.PasswordRecoveryToken(userId,"mobile");
            if(result!=null && otp != null)
            {
                if (!string.IsNullOrEmpty(result.PhoneNo))
                {
                    await SendSms.SendOtp(result.PhoneNo," Password Reset OTP :"+ otp);
                    return "OTP has been sent on register moible number";
                }
                return "moible number is not register";

            }
            
            return "Admission Id does not exist";
        }

        public async Task<UserRegistertion> AdmissionIdExists(string username)
        {
            var userdata = await _userRepository.GetStudentByAdmissionId(username);
            if (userdata != null)
                return userdata;

            return null;
        }
    }
}
