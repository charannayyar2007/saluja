using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School.Api.Controllers
{
    public class ModelClass {
        public Header Header { get; set; }
    }

    public class Header
    {
        public string Name { get; set; }
    }
    public class TeacherController : Controller
    {
        // GET: Teacher
       

        public ActionResult Index(string UserId)
        {

            Header header = new Header();
            header.Name = "Priya";
            ModelClass modelClass = new ModelClass();
            modelClass.Header = header;

            return View();
        }
        public ActionResult MarkAttendence(string UserId)
        {
            Header header = new Header();
            header.Name = "Priya";
            ModelClass modelClass = new ModelClass();
            modelClass.Header = header;

            return View("~/Views/Teacher/MarkAttendence.cshtml", modelClass);
        }
        public ActionResult ViewAttendence(string UserId)
        {
            Header header = new Header();
            header.Name = "Priya";
            ModelClass modelClass = new ModelClass();
            modelClass.Header = header;
            return View("~/Views/Teacher/ViewAttendence.cshtml", modelClass);
        }

        public ActionResult Homework(string UserId)
        {
            Header header = new Header();
            header.Name = "Priya";
            ModelClass modelClass = new ModelClass();
            modelClass.Header = header;
            return View("~/Views/Teacher/Homework.cshtml", modelClass);
        }

    }
}