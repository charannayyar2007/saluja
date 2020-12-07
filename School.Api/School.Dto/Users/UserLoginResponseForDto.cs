using School.Utility.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Dto.Users
{
   public class UserLoginResponseForDto
    {
        //[EmailAddress]
        public string UserId { get; set; }
        public RoleEnum RoleId { get; set; }
        public string Token { get; set; }
    }
}
