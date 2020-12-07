
using AutoMapper;
using School.Helpers.AutoMapper;

namespace School.Application
{
    public abstract class BaseService
    {
        protected readonly IMapper mapper;
        public  BaseService()
        {
            mapper = AutoMapperProfiles.Mappers(); ;
        }
    }
}
