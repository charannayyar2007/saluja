using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.HomeWork
{
   public class BaseAssignment
    {
        public string enrollmentCode { get; set; } //Student AdmissionID and teacher Enroll ID or take userid
        public int? classid { get; set; }
        public int? sectionId { get; set; }
        public int? subjectId { get; set; }
        public int? uploadedby { get; set; }

        public DateTime? uploadeOn { get; set; }
        public string filepath { get; set; }
        public string FileName { get; set; }
        public string AssId { get; set; }

    }

    public class TeacherAssignment : BaseAssignment
    {
        public int Id { get; set; }
        public DateTime? deadlineDate { get; set; }
    }

    public class StudentUploadAssignment : BaseAssignment
    {
        public string Remark { get; set; }
    }

}
