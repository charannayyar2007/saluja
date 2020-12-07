using School.Dto.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Contract
{
   public interface IMastersService
    {
          Task<dynamic> GetAllMasters();

        Task<dynamic> GetAllSubjectMaster(int ClassId);
    }
}
