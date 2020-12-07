using System;

namespace School.Domain.Master
{
   public class SubjectsMaster
    {
        public SubjectsMaster()
        {
            CreateOn = DateTime.Now;
        }
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
