

using AutoMapper;
using Infra.School.Data.Db;
using School.Domain.Student;

namespace Infra.School.Data.AutoMapper
{
    class StudentMapperProfile: Profile
    {
        public StudentMapperProfile()
        {
            CreateMap<Student, StudentDetail>();
        }
    }
}
