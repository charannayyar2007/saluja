using School.Dto.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Dto.Master
{
    public class BaseMasterListForDto
    {
        public List<ClassMasterForDto> ClassesMasters { get; set; }
        public List<SectionMasterForDto> SectionsMasters { get; set; }
        public List<SessionMasterForDto> SessionMasters { get; set; }
        public List<SubjectMasterForDto> SubjectsMasters { get; set; }
        public List<StudenDetailsForDto> Students { get; set; }
    }
}
