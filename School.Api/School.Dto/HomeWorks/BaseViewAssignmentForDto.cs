using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Dto.HomeWorks
{
   public class BaseViewAssignmentForDto
    {
        public int Id { get; set; }   
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Subject { get; set; }
       
        public string uploadOn { get; set; }
        public string LastUploadDate { get; set; }
        public string AssId { get; set; }
        
       
        //public string UserId { get; set; }
    }

    public class TeacherViewAssignmentForDto:BaseViewAssignmentForDto
    {
         public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Status { get; set; }
        //public string Remark { get; set; }
            public List<string> Remarks { get; set; }
        public List<bool?> DownloadCheck { get; set; }
    }
    public class TeacherUploadAssignmentForDto:BaseViewAssignmentForDto
    {
        public string UserId { get; set; }
        public string fileName { get; set; }
        public string filepath { get; set; }

    }

    public class TeacherDownloadAssignmentForDto
    {
        public string fileName { get; set; }
        public string filepath { get; set; }

        public int FileId { get; set; }
    }
}
