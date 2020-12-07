

using School.Dto.Teachers;
using System.Threading.Tasks;

namespace School.Application.Contract
{
   public interface ITeacherService
    {
        Task<TeacherDetailForDto> GetTeacherDetail(string UserId);
        Task<TeacherDetailForDto> AddTeacherDetail(TeacherDetailForDto detail);
    }
}
