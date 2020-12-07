
using System;

namespace School.Domain.Student
{
  public  class StudentDetail
    {
     

        public int Id { get; set; }
        public string admissionId { get; set; }
        public string firstname { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string fathername { get; set; }
        public string motherName { get; set; }
        public int? classId { get; set; }
        public int? sectionId { get; set; }
        public string  ClassName { get; set; }
        public string Section { get; set; }
        public string rollNo { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public Guid UniqueId { get; set; }
        public string Address { get; set; }
        public string DOB { get; set; }
    }
}
