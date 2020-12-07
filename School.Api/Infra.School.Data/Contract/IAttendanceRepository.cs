
using School.Domain.Attendance;
using System.Threading.Tasks;

namespace Infra.School.Data.Contract
{
  public  interface IAttendanceRepository
    {
        Task<MarkAttendace> MarkeAttence(string admission,string presentstatus);
        Task GetAttendaceByClass(int classId, int sectionId);
        //Task<bool> MarkAttendance(string admissionId,string statusType);
    }
}
