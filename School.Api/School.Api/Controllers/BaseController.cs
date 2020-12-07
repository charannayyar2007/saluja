using School.Utility.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace School.Api.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
     
        public async Task<dynamic> GetAllMasterAsync()
        {
            string baseurl = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority; //ConfigurationManager.AppSettings["apiUrl"];
            baseurl = baseurl + "/GetAllMaster";
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session[ConstantKeys.TokenSessionKey]);
                var response = await client.GetStringAsync(baseurl);
                // Parse JSON response.
                return response;
            }
            return null;
        }

        public async Task<dynamic> PostAsync<T>(T obj,string url)
        {
            string baseurl = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority; //ConfigurationManager.AppSettings["apiUrl"];
            baseurl = baseurl + url;
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session[ConstantKeys.TokenSessionKey]);
                var response = await client.PostAsJsonAsync(baseurl,obj);
                // Parse JSON response.
                return response;
            }
            return null;
        }
    }
}