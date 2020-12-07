
using AutoMapper;
using School.Domain.Menu;
using School.Dto.NavigationsMenu;

namespace School.Helpers.AutoMapper
{
 public class NavMapperProfile:Profile
    {
        public NavMapperProfile()
        {
            CreateMap<NavigationMenuAddForDto, NavigationsMenu>();
            CreateMap<NavigationsMenu, NavigationMenuResponseForDto>();
        }
    }
}
