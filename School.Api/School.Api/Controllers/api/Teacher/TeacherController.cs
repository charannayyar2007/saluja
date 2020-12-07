using School.Api.Authorization;
using School.Application.Contract;
using School.Application.FactoryApplicationService;
using School.Application.TeachersService;
using School.Application.UsersService;
using School.Dto.Teachers;
using School.Dto.Users;
using School.Utility.Constant;
using School.Utility.Enum;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace School.Api.Controllers.Teacher
{
    [CustomAuthorize]
    public class TeacherController : ApiController
    {
        private readonly IUserService _userService;

        private readonly ITeacherService _teacherService;
        public TeacherController()
        {
            _userService = ApplicatonServiceFactory<UserRegistrationService>.Create();

            _teacherService = ApplicatonServiceFactory<TeacherServicce>.Create();

        }
        [HttpGet]
        [Route("GetTeacherDetail")]
        public async Task<IHttpActionResult> GetTeacherDetail()
        {
            var userId = HttpContext.Current.User.Identity.Name;
            var result= await _teacherService.GetTeacherDetail(userId);
            return Ok(result);
        }
        [HttpPost]
        [Route("AddTeacher")]
        public async Task<IHttpActionResult> AddTeacher(TeacherDetailForDto detail)
        {
            if (ModelState.IsValid)
            {

                if (!await _userService.UserExists(detail.Email))
                {
                    var result = await _teacherService.AddTeacherDetail(detail);
                    if (result != null)
                    {
                        var userRegister = new UserRegistrationDto()
                        {
                            UserId = result.Email,
                            Password = ConstantKeys.defaultPassword
                        ,
                            RoleId = RoleEnum.teacher
                        };
                        var IsUserIdCreated = await _userService.UserRegistration(userRegister);
                        //if (IsUserIdCreated)
                        //{
                        //    return Ok(result);
                        //}
                    }
                }
                var message = string.Format("User Already Exist");
                HttpError err = new HttpError(message);
                return BadRequest(err.Message);
            }
            return BadRequest(ModelState);
            }
           
        }
}
