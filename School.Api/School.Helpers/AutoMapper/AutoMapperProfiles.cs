using AutoMapper;
using School.Helpers.AutoMapper.Masters;
using School.Helpers.AutoMapper.Teacher;

namespace School.Helpers.AutoMapper
{
   public class AutoMapperProfiles
    {
        public static IMapper Mappers()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserMapperProfiles());
                mc.AddProfile(new StudentMapperProfile());
                mc.AddProfile(new NavMapperProfile());
                mc.AddProfile(new TeacherMapperProfile());
                mc.AddProfile(new HomeWorkMapperProfiles());
                mc.AddProfile(new ClassMapperProfiles());
                mc.AddProfile(new AttendanceMapperProfile());
                mc.AddProfile(new BaseMapperProfiles());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            return mapper;
        }
    }
}
