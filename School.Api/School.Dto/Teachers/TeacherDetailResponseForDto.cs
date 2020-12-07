
using System.ComponentModel.DataAnnotations;

namespace School.Dto.Teachers
{
   public class TeacherDetailForDto
    {
        [Required]
        public string EnrollmentCode { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MidName { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Address1 { get; set; }
        [Required]
        public string Address2 { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Dob { get; set; }
        [Required]
        public string Doj { get; set; }
      
        [Required]
        public string Email { get; set; }
    }
}
