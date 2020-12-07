using Infra.School.Data.Contract;
using Infra.School.Data.Db;
using School.Domain.Teacher;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.School.Data.TeachersRepository
{
    public class TeacherRepository : ITeacherRespository
    {
        public async Task<TeacherDetails> GetTeacherDetail(string userId)
        {
            using (var context = new SchoolErp())
            {
               var results= await context.TeacherDetails.Where(x => x.Email == userId|| x.Phone==userId).FirstOrDefaultAsync();
                if(results!=null)
                {
                    var result = new TeacherDetails()
                    {
                        FirstName = results.firstName,
                        Lastname=results.Lastname,
                        Address1=results.Address1,
                        Address2=results.Address2,
                        Dob=results.Dob,
                        Doj=results.Doj,
                        Email=results.Email,
                        MidName=results.midName,
                        EnrollmentCode=results.enrollmentCode,
                        Phone=results.Phone

                    };
                    return result;
                }
                
                return null;
            }
        }
        public async Task<TeacherDetails> AddTeacherDetail(TeacherDetails deatil)
        {
            using (var context = new SchoolErp())
            {
                
                    var result = new TeacherDetail()
                    {
                        firstName = deatil.FirstName,
                        Lastname = deatil.Lastname,
                        Address1 = deatil.Address1,
                        Address2 = deatil.Address2,
                        Dob = deatil.Dob,
                        Doj = deatil.Doj,
                        Email = deatil.Email,
                        midName = deatil.MidName,
                        enrollmentCode = deatil.EnrollmentCode,
                        Phone = deatil.Phone

                    };
                context.TeacherDetails.Add(result);
               var add= await context.SaveChangesAsync();
                if (add>=1)
                {
                    return deatil;
                }
                
                return null;
            }
        }
    }
}
