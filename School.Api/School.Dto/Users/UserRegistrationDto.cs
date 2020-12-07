using School.Utility.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Dto.Users
{
   public class UserRegistrationDto
    {
        [Required]
    //   [Phone]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public RoleEnum RoleId { get; set; }
    }
}
