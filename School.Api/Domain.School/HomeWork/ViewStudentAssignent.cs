using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.HomeWork
{
   public class ViewStudentAssignent
    {
        public string AssId { get; set; }
        public string Subject { get; set; }
        public string AssignedBy { get; set; }
        //public string LastName { get; set; }
        public string FilePath { get; set; }
        public DateTime? LastSubmissionDate { get; set; }
        public DateTime? Date { get; set; }
        public bool Status { get; set; }
        public int? SubjectId { get; set; }
    }
}
