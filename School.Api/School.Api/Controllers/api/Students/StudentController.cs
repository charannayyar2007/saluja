using School.Api.Authorization;
using School.Application.Contract;
using School.Application.FactoryApplicationService;
using School.Application.StudentServices;
using School.Application.UsersService;
using School.Dto.Students;
using School.Dto.Users;
using School.Utility.Constant;
using School.Utility.Enum;
using System.Threading.Tasks;
using System.Web.Http;

namespace School.Api.Controllers.Students
{
   // [CustomAuthorize]
    public class StudentController : ApiController
    {
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;
        public StudentController()
        {
            _studentService = ApplicatonServiceFactory<StudentService>.Create();
            _userService = ApplicatonServiceFactory<UserRegistrationService>.Create();

        }
        [HttpPost]
        [Route("AddStudent")]
        public async Task<IHttpActionResult> AddStudent(StudentForDto studentDetail)
        {
            if (ModelState.IsValid)
            {
                if (!await _userService.UserExists(studentDetail.Phone))
                {
                    var userRegister = new UserRegistrationDto()
                    {
                        UserId = studentDetail.Phone,
                        Password = ConstantKeys.defaultPassword,
                        RoleId = RoleEnum.student
                    };

                    var uniqueId = await _userService.UserRegistration(userRegister);
                    studentDetail.UniqueId = uniqueId;
                    var result = await _studentService.AddStudent(studentDetail);
                    return Ok(result);

                }
                var message = string.Format("something is worng");

                HttpError err = new HttpError(message);
                return BadRequest(err.MessageDetail);
            }
            return BadRequest(ModelState);
            
           
        }

        [HttpGet]
        [Route("GetStudentDetailsUserId")]
        public async Task<IHttpActionResult> GetStudentDetails(string UserId)
        {
           
            var StudentDetails = await _studentService.GetDetails(UserId);
            return Ok(StudentDetails);
        }

        [HttpGet]
        [Route("GetStudentbyAdmissionId")]
        public async Task<IHttpActionResult> GetStudentByApplicatoinId(string AddmistionId)
        {
            var StudentDetails = await _studentService.GetStudentByApplicationId(AddmistionId);
            return Ok(StudentDetails);
        }
        [HttpGet]
        [Route("GetStudentbyName")]
        public async Task<IHttpActionResult> GetStudentbByName(int classId,int sectionId, string name)
        {
            var StudentDetails = await _studentService.GetStudentByName(classId,sectionId,name);
            return Ok(StudentDetails);
        }
        [HttpGet]
        [Route("GetAllStudent")]
        public async Task<IHttpActionResult> GetAllStudent()
        {
            var StudentDetails = await _studentService.GetAllStudent();
            return Ok(StudentDetails);
        }
    }
}
