using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.School.Data.Contract
{
   public interface IUserPermissionRepository
    {
    bool CheckPermession(string userName,string ActionName,string ControllerName);
        void GetMenuList(string userName);
    }
}
