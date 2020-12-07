using School.Domain.Master;
using School.Dto.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.School.Data.Contract
{
    public interface IMastersRepository
    {
         Task<BaseMastersList> GetAllMaster();

        Task<List<SubjectsMaster>> GetAllSubjectMaster(int ClassId);
    }
}
