
using System.ComponentModel.DataAnnotations;


namespace School.Dto.Users
{
   public class UserLoginDto
    {
        [Required]
        //[EmailAddress]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
                    
    }
}
