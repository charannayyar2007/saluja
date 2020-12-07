using School.Api.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;

namespace School.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
          //  config.Formatters.Add(new CustomJsonFormatter());
            // Web API routes
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            config.MapHttpAttributeRoutes();
             config.MessageHandlers.Add(new TokenValidationHandler());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }

            );
            //   Global.LogMessage = Requestlog.PostToClient;
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

        }
    }
    public class Global
    {
        public delegate void DelLogMessage(string data);
        public static DelLogMessage LogMessage;
    }
}
