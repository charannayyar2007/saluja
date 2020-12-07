using AutoMapper;
using School.Domain.Master;
using School.Dto.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Helpers.AutoMapper.Masters
{
   public class ClassMapperProfiles:Profile
    {
       public ClassMapperProfiles()
        {
            CreateMap<ClassesMaster, ClassMasterForDto>();
            CreateMap<ClassMasterForDto, ClassesMaster>();
        }
    }
}
