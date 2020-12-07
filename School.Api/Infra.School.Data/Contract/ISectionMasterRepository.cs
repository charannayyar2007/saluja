using School.Dto.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.School.Data.Contract
{
    public interface ISectionMasterRepository
    {
        List<SectionMasterForDto> GetAll();
    }
}
