
//using School.ExceptionLogger;
using School.ExceptionLogger;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using static School.ExceptionLogger.DbLogger<School.ExceptionLogger.MSSQLServer>;

namespace School.Api.App_Start
{
    public class GlobalExceptionHandler: ExceptionHandler
    {
        public async override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            // Access Exception using context.Exception;  
            Log.ConnectionStringName = "ConString"; //ConfigurationManager.ConnectionStrings[""].ConnectionString ;

            Log.Info(context.Exception.Message +" "+ context.Exception?.InnerException?.Message +"stackTrace " + context.Exception?.InnerException?.StackTrace
                +"source "+ context.Exception?.Source+ "StackTrace " + context.Exception?.StackTrace);
            // Access Exception using context.Exception;  
            const string errorMessage = "An unexpected error occured";
            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                new
                {
                    Message = errorMessage
                });
         
            response.Headers.Add("X-Error", errorMessage);
            context.Result = new ResponseMessageResult(response);
        }
    }
}