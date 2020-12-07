using Infra.School.Data.Contract;
using Infra.School.Data.DataFactory;
using Infra.School.Data.StudentsRepository;
using School.Application.Contract;
using School.Domain.Student;
using School.Dto.Students;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Application.StudentServices
{
    public class StudentService : BaseService,IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService()
        {
            _studentRepository= FactoryDataLayer<StudentRespository>.Create();
        }
        public async Task<StudentForDto> AddStudent(StudentForDto student)
        {
            var studentTOMap = mapper.Map<StudentDetail>(student);
            StudentForDto resultToMap=null;
         var result= await _studentRepository.AddStudent(studentTOMap);
            if (result != null)
            {
              
                 resultToMap = mapper.Map<StudentForDto>(result);
            
            }
            return resultToMap;
        }

        public async Task<List<StudenDetailsForDto>> GetAllStudent(int start, int end)
        {
          var studentDetails=await  _studentRepository.GetAllStudent(start,end);
          return mapper.Map<List<StudenDetailsForDto>>(studentDetails);

        }
        public async Task<List<StudenDetailsForDto>> GetAllStudent()
        {
            var studentDetails = await _studentRepository.GetAllStudent();
            return mapper.Map<List<StudenDetailsForDto>>(studentDetails);

        }

        public async Task<StudentDashboardDetailsForDto> GetDetails(string UserId)
        {
            var details = await _studentRepository.GetStudentDetails(UserId);
            var studentDetails = mapper.Map<StudentDashboardDetailsForDto>(details);
            return studentDetails;

        }
        public async Task<List<StudentForDto>> GetStudentByApplicationId(string ApplicationId)
        {
            var studentDetails = await _studentRepository.GetStudentList(ApplicationId);
            var studentTOMap = mapper.Map< List<StudentForDto>>(studentDetails);
            return studentTOMap;

        }
        public async Task<List<StudentForDto>> GetStudentByName(int classId, int sectionId,string name )
        {
            var studentDetails = await _studentRepository.GetStudentList(classId,sectionId,name);
            var studentTOMap = mapper.Map<List<StudentForDto>>(studentDetails);
            return studentTOMap;

        }
    }
}
