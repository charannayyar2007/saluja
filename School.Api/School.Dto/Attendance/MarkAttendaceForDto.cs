
using System.ComponentModel.DataAnnotations;

namespace School.Dto.Attendance
{
   public class MarkAttendaceForDto
    {
        [Required(ErrorMessage="Admission Id is Required")]
     
        public string EnrollmentId { get; set; }
        public string MarkeType { get; set; }
    }
}
