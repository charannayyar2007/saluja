
using MailAndSmsService.Sms;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using School.Api.Authorization;
using School.Dto.Master;
using School.Dto.Students;
using School.Helpers.HttpClientHelper;
using School.Utility.Constant;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace School.Api.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        [HttpPost]
        public ActionResult Index(string token)
        {
          
            Session[ConstantKeys.TokenSessionKey] = token;
         
            return View();
        }
        [UserAuthenticationFilter]
        public ActionResult Index()
        {
                return View();
        }
        [UserAuthenticationFilter]
        public async Task<ActionResult> AttendaceView()
        {
            BaseMasterListForDto objto;
            if (Session[ConstantKeys.MasterSession] == null)
            {
                var result = await GetAllMasterAsync();
                if(result!=null)
                {
                    Session[ConstantKeys.MasterSession] = result;
                    objto= JsonConvert.DeserializeObject<BaseMasterListForDto>(result);
                    return View(objto);
                }
            }
            objto = JsonConvert.DeserializeObject<BaseMasterListForDto>(Session[ConstantKeys.MasterSession].ToString());

            return View(objto);
        }
        [UserAuthenticationFilter]
        public async Task<ActionResult> AddStudent()
        {
            BaseMasterListForDto objto;
            if (Session[ConstantKeys.MasterSession] == null)
            {
                var result = await GetAllMasterAsync();
                if (result != null)
                {
                    Session[ConstantKeys.MasterSession] = result;
                    objto = JsonConvert.DeserializeObject<BaseMasterListForDto>(result);
                    return View(objto);
                }
            }
            objto = JsonConvert.DeserializeObject<BaseMasterListForDto>(Session[ConstantKeys.MasterSession].ToString());

            return View(objto); 
        }

        [UserAuthenticationFilter]
        [HttpPost]
        public async Task<ActionResult> AddStudent(StudentForDto std)
        {
            BaseMasterListForDto objto;
            if (ModelState.IsValid)
            {
               await PostAsync(std, "/AddStudent");
            }
                
           
            objto = JsonConvert.DeserializeObject<BaseMasterListForDto>(Session[ConstantKeys.MasterSession].ToString());

            return View(objto); 
        }
        public  ActionResult LogOut()
        {
            Session.Clear();
            return Redirect("/");
        }
    }
}