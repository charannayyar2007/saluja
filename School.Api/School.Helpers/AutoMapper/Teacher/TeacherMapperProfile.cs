using AutoMapper;
using School.Domain.Teacher;
using School.Dto.Teachers;

namespace School.Helpers.AutoMapper.Teacher
{
    class TeacherMapperProfile:Profile
    {
        public TeacherMapperProfile()
        {
            CreateMap<TeacherDetails, TeacherDetailForDto>();
            CreateMap<TeacherDetailForDto, TeacherDetails>();
        }
    }
}
