
using School.Application.Contract;
using School.Application.FactoryApplicationService;
using School.Application.NavigationsMenuService;
using School.Dto.NavigationsMenu;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace School.Api.Controllers.Navigation
{
    public class NavigationMenuController : ApiController
    {
        private readonly INavigationMenuService _navService;
        public NavigationMenuController()
        {
            _navService = ApplicatonServiceFactory<NavigationMenuService>.Create();

        }
        [HttpPost]
        [Route("addNav")]
        public async Task<IHttpActionResult> AddNavigation(NavigationMenuAddForDto userRegitration)
        { 
              var result=  await _navService.AddNavigation(userRegitration);
                return Ok(result);
        }
        [HttpGet]
        [Route("getNav")]
        public async Task<IHttpActionResult> GetNavigation()
        {
          var userId=  HttpContext.Current.User.Identity.Name;
            var result = await _navService.GetNavigation(userId);
            return Ok(result);//
        }
    }
}
