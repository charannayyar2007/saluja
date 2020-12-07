using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain
{
  public  class AttendanceView
    {
        public string Name { get; set; }
        public string AdmissionId { get; set; }
        public string PresentStatus { get; set; }
        public int Days { get; set; }
      
    }
}
