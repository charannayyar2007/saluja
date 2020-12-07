using System;

namespace School.Domain.HomeWork
{
    public class Assignment
    {
     public   Assignment() { uploadeOn = DateTime.Now; }
        public int id { get; set; }
        public string enrollmentCode { get; set; } //Student AdmissionID and teacher Enroll ID or take userid
        public int? classid { get; set; }
        public int? sectionId { get; set; }
        public int? subjectId { get; set; }
        public int? uploadedby { get; set; } //define role either teacher or student
        public DateTime? deadlineDate { get; set; }
        public DateTime? uploadeOn { get; set; }
        public string filepath { get; set; }
        public string fileName { get; set; }
        public string AssId { get; set; }
        public string Remark { get; set; }
    }
}
