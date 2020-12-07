
using School.Dto.Students;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Application.Contract
{
   public interface IStudentService
    {
        Task<StudentForDto> AddStudent(StudentForDto student);
        Task<List<StudentForDto>> GetStudentByApplicationId(string ApplicationId);
        Task<List<StudentForDto>> GetStudentByName(int classId, int sectionId, string name);
        Task<StudentDashboardDetailsForDto> GetDetails(string UserId);
        Task<List<StudenDetailsForDto>> GetAllStudent(int start, int end);
        Task<List<StudenDetailsForDto>> GetAllStudent();
    }
}
