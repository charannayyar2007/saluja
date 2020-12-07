using AutoMapper;
using Infra.School.Data.Db;
using School.Domain.Menu;
using School.Domain.Student;


namespace Infra.School.Data.AutoMapper
{
    class NavigationsMenuMapperProfile :Profile
    {
        public NavigationsMenuMapperProfile()
        {
            CreateMap<NavigationMenu, NavigationsMenu>();
        }
    }
}
