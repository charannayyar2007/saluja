using School.Application.AttendancesService;
using School.Application.Contract;
using School.Application.HomeWorksService;
using School.Application.Masters;
using School.Application.NavigationsMenuService;
using School.Application.ReportCardService;
using School.Application.StudentServices;
using School.Application.UsersService;
using School.Dto.Master;
using School.Dto.Students;
using Unity;
namespace School.Application.FactoryApplicationService
{
   public static class  ApplicatonServiceFactory<AnyType>
    {
        private static IUnityContainer ObjectsofOurProjects = null;
     
        public static AnyType Create()
        {
            //Lazy loading. Eager loading
          
            if (ObjectsofOurProjects == null)
            {
                ObjectsofOurProjects = new UnityContainer();

                ObjectsofOurProjects.RegisterType<IUserService,
                    UserRegistrationService>();
                ObjectsofOurProjects.RegisterType<IUserPermissionService,
                  UserPermissionService>();
                ObjectsofOurProjects.RegisterType<IStudentService,
                StudentService>();
                ObjectsofOurProjects.RegisterType<INavigationMenuService,
              NavigationMenuService>();
                ObjectsofOurProjects.RegisterType<IHomeWorkService, HomeWorkService>();
                ObjectsofOurProjects.RegisterType<IMastersService, 
                    MastersService>();
                ObjectsofOurProjects.RegisterType<IAttendanceService,
                  AttendanceService>();
                ObjectsofOurProjects.RegisterType<IEvaluationService,
                 EvaluationService>();
            }
            //  RIP Replace If with Poly
            return ObjectsofOurProjects.Resolve<AnyType>();
        }
    }
}
