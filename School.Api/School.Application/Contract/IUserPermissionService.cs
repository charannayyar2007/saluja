using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Contract
{
  public interface IUserPermissionService
    {
     bool IsUserAllow(string UserId, string ActionName, string Controller);
    }
}
