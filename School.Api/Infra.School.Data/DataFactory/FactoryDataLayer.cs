using Infra.School.Data.AttendancesRepository;
using Infra.School.Data.Contract;
using Infra.School.Data.Db;
using Infra.School.Data.HomeWorksRepository;
using Infra.School.Data.MasterRepository;
using Infra.School.Data.MenusAssingmentRepository;
using Infra.School.Data.ReportCardRepository;
using Infra.School.Data.StudentsRepository;
using Infra.School.Data.UserRepository;
using School.Domain;
using School.Domain.Master;
using School.Domain.Student;
using System.Data;
using Unity;

namespace Infra.School.Data.DataFactory
{
    public static class FactoryDataLayer<AnyType>
    {
        private static IUnityContainer ObjectsofOurProjects = null;

        public static AnyType Create()
        {
            //Lazy loading. Eager loading
            if (ObjectsofOurProjects == null)
            {
                ObjectsofOurProjects = new UnityContainer();

                ObjectsofOurProjects.RegisterType<IUserRepository,
                    UsersRepository>();
                ObjectsofOurProjects.RegisterType<IUserPermissionRepository,
                   UserPermissionRepository>();
                ObjectsofOurProjects.RegisterType<IStudentRepository,
                  StudentRespository>();
                ObjectsofOurProjects.RegisterType<INavigationsMenuRespository,
              NavigationsMenuRepository>();
                ObjectsofOurProjects.RegisterType<IHomeWorkRepository, HomeworkRepository>();
                ObjectsofOurProjects.RegisterType<IMastersRepository, MastersRepository>();
                ObjectsofOurProjects.RegisterType<IAttendanceRepository, AttendanceRepository>();
                ObjectsofOurProjects.RegisterType<IRepository<DataTable>, AttendanceRepository>();
                ObjectsofOurProjects.RegisterType<IEvaluationRepository, EvaluationRepository>();

                //   ObjectsofOurProjects.RegisterType<ISectionMasterRepository, SectionMasterRepository>();

            }
            //  RIP Replace If with Poly
            return ObjectsofOurProjects.Resolve<AnyType>();
        }
    }
 }
