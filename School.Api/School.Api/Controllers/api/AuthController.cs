using School.Api.Authorization;
using School.Application.Contract;
using School.Application.FactoryApplicationService;
using School.Application.UsersService;
using School.Dto.Users;
using School.Utility.Constant;
using School.Utility.Enum;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace School.Api.Controllers
{
    
    public class AuthController : ApiController
    {
       private readonly IUserService _userService;
       public AuthController()
        {
             _userService = ApplicatonServiceFactory<UserRegistrationService>.Create();

        }
        // GET api/values
        [HttpPost]
        [Route("signup")]
        public async Task<IHttpActionResult> UserRegister(UserRegistrationDto userRegitration)
        {
            if (ModelState.IsValid)
            {
                if (!await _userService.UserExists(userRegitration.UserId))
                {
                    await _userService.UserRegistration(userRegitration);
                    string token = createToken(userRegitration.UserId, userRegitration.RoleId.ToString());
                    //return the token
                    return Ok<string>(token);

                }
                var message = string.Format("UserId= {0} already Exist", userRegitration.UserId);

                HttpError err = new HttpError(message);
                return BadRequest(err.Message);
            }
            return BadRequest();


        }
        //[HttpGet]
        //[Route("userDetail")]

        //public async Task<IHttpActionResult> UserDetail()
        //{
        //    var userId = HttpContext.Current.User.Identity.Name;
        //    var result = await _userService.GetUserDetail(string userId);
        //    return Ok(result);
        //}
        [HttpPost]
        [Route("Login")]
        public async Task<IHttpActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            if (ModelState.IsValid)
            {
                var userFromRepo = await _userService.Login(userLogin);

                if (userFromRepo == null)
                    return BadRequest("User Name And Password is Not valid");// Unauthorized();

                string token = createToken(userLogin.UserId, userFromRepo.RoleId.ToString());
                //return the token
                return Ok(new { token = token, roleId = userFromRepo.RoleId.ToString(), userId = userLogin.UserId });
            }
            return BadRequest("User Name And Password is Not valid");
        }
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IHttpActionResult> ForgotPassword([FromBody] string Username)
        {
            if (!string.IsNullOrEmpty(Username))
            {
                if (await _userService.UserExists(Username))
                {
                    var userFromRepo = await _userService.PasswordRecoveryTokenGenerateApp(Username);
                    if (!string.IsNullOrEmpty(userFromRepo))
                    {
                        return Ok(userFromRepo);
                    }
                 //   var userFromRepo = await _userService.PasswordRecoveryTokenGenerate(Username);
                  //  return Ok<bool>(userFromRepo);
                }
            }
            //return the token
            return BadRequest("user dose not exist");
        }
        [HttpPost]
        [Route("ForgotPasswordApp")]
        public async Task<IHttpActionResult> ForgotPasswordApp([FromUri] string Username)
        {
            if (!string.IsNullOrEmpty(Username))
            {
               var userResult= await _userService.AdmissionIdExists(Username);
                if (userResult!=null)
                {
                    var userFromRepo = await _userService.PasswordRecoveryTokenGenerateApp(userResult.userId);
                    if (!string.IsNullOrEmpty(userFromRepo))
                    {
                        return Ok(userFromRepo);
                    }
                    //string userid = await _userService.IsValidToken(userFromRepo, userResult.userId);
                    //if (!string.IsNullOrEmpty(userid))
                    //{
                    //    UserPasswordResetForDto obj = new UserPasswordResetForDto();
                    //    obj.Password = userDetail.Password;
                    //    obj.Repassword = userDetail.Password;
                    //    obj.Token = userFromRepo;
                    //    obj.UserId = userId;
                    //    await _userService.UpdatePassword(obj);
                    //    return Ok<string>(userFromRepo);
                    //}
                }
            }
            //return the token
            return BadRequest("user dose not exist");
        }
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(UserPasswordResetForDto pass)
        {
            if (ModelState.IsValid)
            {
                string userid = await _userService.IsValidToken(pass.Token,pass.UserId);
                if (!string.IsNullOrEmpty(userid))
                {
                    pass.UserId = userid;
                    await _userService.UpdatePassword(pass);
                    return Ok("Password has been updated");
                }
                return BadRequest("OTP is not valid");
            }
            return BadRequest("please try again");
        }
        private string createToken(string username,string roleId)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(7);
            string issuer = ConfigurationManager.AppSettings["issuer"];
            string audience = ConfigurationManager.AppSettings["audience"]; 
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username,ClaimTypes.Role,roleId),
                 new Claim(ClaimTypes.Role,roleId),

            });

            const string sec = ConstantKeys.seckey;
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt ,
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: issuer, audience: audience, subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);
                
            return tokenString;
        }

    }
}
