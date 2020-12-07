
using School.Api.Authorization;
using School.Application.AttendancesService;
using School.Application.Contract;
using School.Application.FactoryApplicationService;
using School.Dto.Attendance;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace School.Api.Controllers.api
{
    [CustomAuthorize]
    public class AttendanceController : ApiController
    {
        private readonly IAttendanceService _attenaceService;
        public AttendanceController()
        {
            _attenaceService = ApplicatonServiceFactory<AttendanceService>.Create();

        }
        [HttpPost]
        [Route("MarkAttendace")]
        public async Task<IHttpActionResult> MarkAttendace([FromBody] MarkAttendaceForDto userLogin)
        {
            if (ModelState.IsValid)
            {
                var result = await _attenaceService.MarkAttendance(userLogin.EnrollmentId, userLogin.MarkeType);
                return Ok(result);
            }

            return BadRequest(ModelState);
        }
        [HttpGet]
        [Route("ViewAttendace")]
        public IHttpActionResult ViewAttendace(int classId, int sectionId, int month, string sessionYear)
        {
            var result = _attenaceService.ViewAttendance(classId, sectionId, month, sessionYear);
            return Ok(result);
        }
        [HttpGet]
        [Route("ViewAttendaceByStudentId")]
        public IHttpActionResult ViewAttendaceByStudentId(string admissionId, int month, int sessionYear)
        {
            var result = _attenaceService.ViewAttendance(admissionId,month,sessionYear);
            return Ok(result);
        }
        [HttpGet]
        [Route("ViewAttendaceByClass")]
        public IHttpActionResult ViewAttendaceByClass(int classId, int sectionId)
        {
            var result = _attenaceService.ViewAttendance(classId,sectionId);
            return Ok(result);
        }
    }
}
