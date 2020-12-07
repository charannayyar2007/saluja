
using Infra.School.Data.Contract;
using Infra.School.Data.DataFactory;
using Infra.School.Data.TeachersRepository;
using School.Application.Contract;
using School.Domain.Teacher;
using School.Dto.Teachers;
using System.Threading.Tasks;

namespace School.Application.TeachersService
{
    public class TeacherServicce :BaseService, ITeacherService
    {
        private readonly ITeacherRespository _teacherRepository;

        public TeacherServicce()
        {
            _teacherRepository = FactoryDataLayer<TeacherRepository>.Create();

        }
        public async Task<TeacherDetailForDto> GetTeacherDetail(string UserId)
        {
           var result = await _teacherRepository.GetTeacherDetail(UserId);
            if (result != null)
               return mapper.Map<TeacherDetailForDto>(result);
            return null;
        }
        public async Task<TeacherDetailForDto> AddTeacherDetail(TeacherDetailForDto detail)
        {
            var map = mapper.Map<TeacherDetails>(detail);
            var result = await _teacherRepository.AddTeacherDetail(map);
            if (result != null)
                return mapper.Map<TeacherDetailForDto>(result);
            return null;
        }
    }
}
