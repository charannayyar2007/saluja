using AutoMapper;

namespace Infra.School.Data.AutoMapper
{
    class DbAutoMapperProfiles
    {
        public static IMapper Mappers()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new NavigationsMenuMapperProfile());
                mc.AddProfile(new StudentMapperProfile());
                mc.AddProfile(new MasterMapperProfiles());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            return mapper;
        }
    }
}
