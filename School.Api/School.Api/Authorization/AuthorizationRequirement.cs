

using School.Application.Contract;
using School.Application.FactoryApplicationService;
using School.Application.UsersService;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace School.Api.Authorization
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        private  IUserPermissionService _userPermissionService;
        public CustomAuthorize(params string[] roles)
        {
            this.allowedroles = roles;
           
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            _userPermissionService = ApplicatonServiceFactory<UserPermissionService>.Create();
            if (!base.IsAuthorized(actionContext)) return false;
            var actionName = actionContext.ActionDescriptor.ActionName;
            var ControllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var isAuthorized = true;
           
            // Do some work here to determine if the user has the
            // correct permissions to be authorized anywhere this
            // attribute is used. Assume the username is how
            // you'd link back to a custom user permission scheme.
            var username = HttpContext.Current.User.Identity.Name;
            isAuthorized= _userPermissionService.IsUserAllow(username, actionName, ControllerName);
     

            return isAuthorized;
        }
     
    }
}
   

