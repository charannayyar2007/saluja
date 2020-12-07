
using School.Dto.Attendance;
using System.Threading.Tasks;

namespace School.Application.Contract
{
   public interface IAttendanceService
    {
       dynamic ViewAttendance(int? classId=null,int? sectionId=null,int? month=null, string sessionYear=null);
        dynamic ViewAttendance(string AdmissionId ,  int month, int sessionYear);
        dynamic ViewAttendance(int classId, int sectionId);
        Task<MarkAttendanceResposeForDto> MarkAttendance(string AdmissionId, string Pstatus);
    }
}
