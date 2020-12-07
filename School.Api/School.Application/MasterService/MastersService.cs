using Infra.School.Data.Contract;
using Infra.School.Data.DataFactory;
using Infra.School.Data.MasterRepository;
using School.Application.Contract;
using School.Domain.Master;
using School.Dto.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Masters
{
    public class MastersService : BaseService, IMastersService
    {
        private readonly IMastersRepository masterRepository;

        public MastersService()
        {
            masterRepository = FactoryDataLayer<MastersRepository>.Create();
        }

        public async Task<dynamic> GetAllMasters()
        {
           return await masterRepository.GetAllMaster();
        }

        public async Task<dynamic> GetAllSubjectMaster(int ClassId)
        {
            var sub= await masterRepository.GetAllSubjectMaster(ClassId);
           // var subject = mapper.Map<List<SubjectMasterForDto>>(sub);
            return sub;
        }
    }
}
