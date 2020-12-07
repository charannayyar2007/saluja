
using Infra.School.Data.Db;
using School.Domain.Student;
using School.Dto.Students;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.School.Data.Contract
{
  public  interface IStudentRepository
    {
      Task<StudentDetail> AddStudent(StudentDetail studentDetail);
      Task<bool> IsAdmissionIdExist(string id);
        Task<StudentDashboardDetails> GetStudentDetails(string UserId);
        Task<List<StudentDetail>> GetStudentList(int classId, int sectionId, string Name);
        Task<List<StudentDetail>> GetStudentList(string id);
        Task<List<StudentDetail>> GetAllStudent(int start, int end);
        Task<List<StudentDetail>> GetAllStudent();
    }
}
