using AutoMapper;
using Infra.School.Data.AutoMapper;
using Infra.School.Data.Contract;
using Infra.School.Data.Db;
using School.Domain.Master;
using School.Dto.Master;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.School.Data.MasterRepository
{
    public class MastersRepository : IMastersRepository
    {
        private readonly IMapper mapper;
        public MastersRepository()
        {
            mapper = DbAutoMapperProfiles.Mappers(); ;
        }
         public async Task<BaseMastersList> GetAllMaster()
        {
            using (var entity = new SchoolErp())
            {
                BaseMastersList obj = new BaseMastersList();
               
                var classRes =await entity.ClassMasters.ToListAsync();
                var section = await entity.Sections.ToListAsync();
               // var subject = await entity.SubjectMasters.ToListAsync();
                var session = await entity.SessionMasters.ToListAsync();
                //  var  = entity.ClassMasters.ToList();
                List<ClassesMaster> clslist=(mapper.Map<List<ClassesMaster>>(classRes));
                List<SectionsMaster>secList=(mapper.Map<List<SectionsMaster>>(section));
              //  List<SubjectsMaster>subjects=(mapper.Map<List<SubjectsMaster>>(subject));
                List<SessionsMaster>sessList=(mapper.Map<List<SessionsMaster>>(session));
                obj.ClassesMasters = clslist;
                obj.SectionsMasters = secList;
              //  obj.SubjectsMasters = subjects;
                obj.SessionMasters = sessList;
                return obj;

            }
        }

        public async Task<List<SubjectsMaster>> GetAllSubjectMaster(int ClassId)
        {
            using (var entity = new SchoolErp())
            {
                  var subject = await (from map in entity.ClassmaptoSubjects
                                 join sub in entity.SubjectMasters on map.SubjectId equals sub.id
                                 where map.ClassId == ClassId
                                 select new SubjectsMaster()
                                 { Id=sub.id, Subject=sub.Subject }).ToListAsync();
                if (subject != null)
                {
                    return subject;
                }
                return null;


           
            }
        }

       
    }
}
