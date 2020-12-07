using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Dto.Students
{
   public class StudentAddForDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime Dob { get; set; }
        [Required]
        public string AdmissionId { get; set; }
        [Required]
        public string FatherName { get; set; }
        [Required]
        public string MotherName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string RollNumber { get; set; }
        [Required]
        public int ClassId { get; set; }
        [Required]
        public int SectionId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

        public Guid UniqueId { get; set; }

    }
}
