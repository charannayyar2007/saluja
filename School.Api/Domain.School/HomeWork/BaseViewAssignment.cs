using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.HomeWork
{
    public class BaseViewAssignment
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Subject { get; set; }
       
        public string uploadOn { get; set; }
        public string LastUploadDate { get; set; }
        public bool Status { get; set; }
        // public string AssId { get; set; }


        //public string UserId { get; set; }
    }

    public class TeacherViewAssignment : BaseViewAssignment
    {
        public string FirstName { get; set; }//Student Name
        public string LastName { get; set; }
       
        //public string Remark{get;set;}
     //   public int? AssignmentId { get; set; }
        public List<string> Remarks { get; set; }
        public List<bool?> DownloadCheck { get; set; }
        //public List<string> FileNames { get; set; }
    }
    public class TeacherUploadAssignment : BaseViewAssignment
    {
        public string fileName { get; set; }
        public string filepath { get; set; }
    }
    public class TeacherDownloadAssignment 
    {
        //public int Id { get; set; }
        public string fileName { get; set; }
        public string filepath { get; set; }
        public int FileId { get; set; }
        //public string Remark { get; set; }
        //public int? AdmissionId { get; set; }
        //public bool? DownloadCheck { get; set; }
    }
}
