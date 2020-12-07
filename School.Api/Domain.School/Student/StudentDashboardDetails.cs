using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Student
{
    public class StudentDashboardDetails
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string AdmissionId { get; set; }
        public string FatherName { get; set; }

        public string RollNumber { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public DateTime? Dob { get; set; }
        public string Adress { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string photo { get; set; }
    }
}
