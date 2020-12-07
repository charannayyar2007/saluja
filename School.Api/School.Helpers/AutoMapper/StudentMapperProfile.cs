using AutoMapper;
using School.Domain.Student;
using School.Dto.Students;


namespace School.Helpers.AutoMapper
{
   public class StudentMapperProfile :Profile
    {
        public StudentMapperProfile()
        {
            CreateMap<StudentForDto, StudentDetail>();
            CreateMap<StudentDetail, StudentForDto>();
            CreateMap<StudentDetail, StudenDetailsForDto>();
            CreateMap<StudentDashboardDetails, StudentDashboardDetailsForDto>();
            CreateMap<StudentDashboardDetailsForDto, StudentDashboardDetails>();
        }
    }
}
