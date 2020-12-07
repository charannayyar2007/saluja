using AutoMapper;
using Infra.School.Data.Db;
using School.Domain.Master;

namespace Infra.School.Data.AutoMapper
{
    class MasterMapperProfiles : Profile
    {
        public MasterMapperProfiles()
        {
            CreateMap<ClassMaster, ClassesMaster>();
            CreateMap<SubjectMaster, SubjectsMaster>();
            CreateMap<Section, SectionsMaster>();
            CreateMap<SessionMaster, SessionsMaster>();
        }
    }
}
