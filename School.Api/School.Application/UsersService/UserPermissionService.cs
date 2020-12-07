
using Infra.School.Data.Contract;
using Infra.School.Data.DataFactory;
using Infra.School.Data.UserRepository;
using School.Application.Contract;
using System.Threading.Tasks;

namespace School.Application.UsersService
{
    public class UserPermissionService : IUserPermissionService
    {

        private readonly IUserPermissionRepository _userPermssionRepository;
        public UserPermissionService()
        {
            _userPermssionRepository = FactoryDataLayer<UserPermissionRepository>.Create();

        }
        public bool IsUserAllow(string UserId, string ActionName, string Controller)
        {
           return _userPermssionRepository.CheckPermession(UserId, ActionName, Controller);
        }
    }
}
