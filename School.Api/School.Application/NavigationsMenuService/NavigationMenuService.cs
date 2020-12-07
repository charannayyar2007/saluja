using Infra.School.Data.Contract;
using Infra.School.Data.DataFactory;
using Infra.School.Data.MenusAssingmentRepository;
using School.Application.Contract;
using School.Domain.Menu;
using School.Dto.NavigationsMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.NavigationsMenuService
{
    public class NavigationMenuService : BaseService, INavigationMenuService
    {
        private readonly INavigationsMenuRespository _navigationRepository;
        public NavigationMenuService()
        {
            _navigationRepository = FactoryDataLayer<NavigationsMenuRepository>.Create();
        }
        public async Task<List<NavigationMenuResponseForDto>> AddNavigation(NavigationMenuAddForDto nav)
        {
            var mapResult = mapper.Map<NavigationsMenu>(nav);
          var results= await _navigationRepository.AddMenu(mapResult);
           
           return mapper.Map<List<NavigationMenuResponseForDto>>(results);

        }

        public async Task<List<NavigationMenuAddForDto>> GetNavigation(string userName)
        {
         var result=  await _navigationRepository.GetMenu(userName);
            var mapp = mapper.Map<List<NavigationMenuAddForDto>>(result);
            return mapp;
        }
    }
}
