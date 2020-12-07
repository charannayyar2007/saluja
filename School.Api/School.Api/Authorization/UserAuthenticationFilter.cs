using School.Utility.Constant;
using System;
using System.ServiceModel.Channels;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace School.Api.Authorization
{
    public class UserAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //Check Session is Empty Then set as Result is HttpUnauthorizedResult 
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session[ConstantKeys.TokenSessionKey])))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //We are checking Result is null or Result is HttpUnauthorizedResult 
            // if yes then we are Redirect to Error View
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.HttpContext.Response.Redirect("/");
            }
        }
    }
}