using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Attendance
{
   public class ClassWiseAttenceReport
    {
        public string StudentName { get; set; }
        public string AdmissionId { get; set; }
        public string RollNo { get; set; }
        public string PresentStatus { get; set; }
        public DateTime Date { get; set; }
        public int?  ClassId { get; set; }
        public int? SectionId { get; set; }
    }
}
