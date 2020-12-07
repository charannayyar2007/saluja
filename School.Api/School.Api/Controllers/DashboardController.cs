
using System.Web.Mvc;

namespace School.Api.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
        
            return View();
        }
        public ActionResult TeacherDashboard()
        {
            return View();
        }

    }
}