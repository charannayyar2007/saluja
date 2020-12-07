using School.Domain.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.School.Data.Contract
{
   public interface INavigationsMenuRespository
    {
        Task<List<NavigationsMenu>> AddMenu(NavigationsMenu menus);
        Task<List<NavigationMenuResponse>> GetMenu(string userName);
    }
}
