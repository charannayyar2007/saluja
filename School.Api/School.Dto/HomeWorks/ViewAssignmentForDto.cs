using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Dto.HomeWorks
{
    public class ViewAssignmentForDto
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string Lastname { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Subject { get; set; }
        public string fileName { get; set; }
        public string filepath { get; set; }
        public string uploadOn { get; set; }
        public string LastUploadDate { get; set; }
        public bool? uploadstatus { get; set; }
        public string AssId { get; set; }
        public string Remark { get; set; }
        public int? SubjectId { get; set; }
        public string UserId { get; set; }
    }
}
