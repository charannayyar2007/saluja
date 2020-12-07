
using School.Domain.Teacher;
using System.Threading.Tasks;

namespace Infra.School.Data.Contract
{
   public interface ITeacherRespository
    {
     Task<TeacherDetails> GetTeacherDetail(string userId);
        Task<TeacherDetails> AddTeacherDetail(TeacherDetails deatil);
    }
}
