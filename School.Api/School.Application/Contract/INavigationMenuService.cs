
using School.Dto.NavigationsMenu;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Application.Contract
{
   public interface INavigationMenuService
    {
      Task<List<NavigationMenuResponseForDto>>  AddNavigation(NavigationMenuAddForDto nav);
     Task<List<NavigationMenuAddForDto>> GetNavigation(string userName);

    }
}
