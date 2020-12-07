
using System.Collections.Generic;

namespace School.Domain.Master
{
   public class BaseMastersList
    {
        public List<AttendanceMaster> AttendaceMasters { get; set; }
        public List<ClassesMaster> ClassesMasters { get; set; }
        public List<HolidayMaster> HolidayMasters { get; set; }
        public List<QualificationMaster> QualificationMasters { get; set; }
        public List<SectionsMaster> SectionsMasters { get; set; }
        public List<SessionsMaster> SessionMasters { get; set; }
        public List<SubjectsMaster> SubjectsMasters { get; set; }
    }
}
