using System;
namespace School.Domain.Users
{
    public class UserRegistertion
    {
        public UserRegistertion() {
            lastLogin = DateTime.Now;
            createOn = DateTime.Now;
            UniqueId = Guid.NewGuid();
                }
        public int id { get; set; }
        public string userId { get; set; }
        public DateTime createOn { get; set; }
        public bool? isActive { get; set; }
        public bool? isDelete { get; set; }
        public int? roleId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime lastLogin { get; set; }
        public int? numberOfFiledLogin { get; set; }
        public string createBy { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public Guid UniqueId { get; set; }
    }
}
