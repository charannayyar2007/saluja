using School.Dto.Users;
using School.Helpers.HttpClientHelper;
using School.Utility.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.DynamicData;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace School.Api.Controllers
{
    

    public class LoginController : Controller
    {
        // GET: Login
    //static HttpClient client = new HttpClient();
        public ActionResult Index()
        {
            

            return View();
        }
        [HttpPost]
        public ActionResult LoginRequest(UserLoginDto user)
        {
            UserLoginDto obj = new UserLoginDto();

            // Add an Accept header for JSON format.
            //HttpClientRequest<UserLoginDto>.client.DefaultRequestHeaders.Accept.Add(
            //new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer qaPmk9Vw8o7r7UOiX-3b-8Z_6r3w0Iu2pecwJ3x7CngjPp2fN3c61Q_5VU3y0rc-vPpkTKuaOI2eRs3bMyA5ucKKzY1thMFoM0wjnReEYeMGyq3JfZ-OIko1if3NmIj79ZSpNotLL2734ts2jGBjw8-uUgKet7jQAaq-qf5aIDwzUo0bnGosEj_UkFxiJKXPPlF2L4iNJSlBqRYrhw08RK1SzB4tf18Airb80WVy1Kewx2NGq5zCC-SCzvJW-mlOtjIDBAQ5intqaRkwRaSyjJ_MagxJF_CLc4BNUYC3hC2ejQDoTE6HYMWMcg0mbyWghMFpOw3gqyfAGjr6LPJcIly__aJ5__iyt-BTkOnMpDAZLTjzx4qDHMPWeND-TlzKWXjVb5yMv5Q6Jg6UmETWbuxyTdvGTJFzanUg1HWzPr7gSs6GLEv9VDTMiC8a5sNcGyLcHBIJo8mErrZrIssHvbT8ZUPWtyJaujKvdgazqsrad9CO3iRsZWQJ3lpvdQwucCsyjoRVoj_mXYhz3JK3wfOjLff16Gy1NLbj4gmOhBBRb8rJnUXnP7rBHs00FAk59BIpKLIPIyMgYBApDCut8V55AgXtGs4MgFFiJKbuaKxq8cdMYEVBTzDJ-S1IR5d6eiTGusD5aFlUkAs9NV_nFw");

            // List data response.

            HttpResponseMessage response = HttpClientRequest<UserLoginDto>.CreateProductAsync(user,"/Login").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsAsync<UserLoginResponseForDto>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
               if(dataObjects.RoleId==RoleEnum.admin)
                {

                    return RedirectToRoute("/Admin/index");
                }
                else if (dataObjects.RoleId == RoleEnum.Coordinator)
                {

                    return RedirectToRoute("/Coordinator/index");

                }
                else if (dataObjects.RoleId == RoleEnum.student)
                {

                    return RedirectToRoute("/Student/Index");
                }
                else if (dataObjects.RoleId == RoleEnum.teacher)
                {

                    return RedirectToRoute("/Teacher/Index");
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
              return View(); ;
        }
    }
}