using Infra.School.Data.Contract;
using Infra.School.Data.Db;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.School.Data.UserRepository
{
    public class UserPermissionRepository : IUserPermissionRepository
    {
        public bool CheckPermession(string userName, string ActionName, string ControllerName)
        {
            using (var entity= new SchoolErp())
            {


                var result = entity.Proc_CheckPermisssion(userName, ActionName, ControllerName);
                if (result != null)
                {
                    return true;
                }
            }
            return false;
        }

        public void GetMenuList(string userName)
        {
            using (var entity = new SchoolErp())
            {


               // var result = entity.Proc_GetAssingNavList(userName).ToList();
               
            };
        }
    }
}
