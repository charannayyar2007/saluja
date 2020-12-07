using System;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace School.Dto.HomeWorks
{
  public  class AssignmentForDto
    {
        public int Id { get; set; }
        public string enrollmentCode { get; set; } //Student AdmissionID and teacher Enroll ID or take userid
        public int? classid { get; set; }
        public int? sectionId { get; set; }
        public int? subjectId { get; set; }
        public int? uploadedby { get; set; }
        public DateTime? deadlineDate { get; set; }
        public DateTime? uploadeOn { get; set; }
        public string filepath { get; set; }
        public string FileName { get; set; }
        public string AssId { get; set; }
        public string Remark { get; set; }

    }
}
