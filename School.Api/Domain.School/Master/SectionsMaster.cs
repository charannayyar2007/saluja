using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Master
{
    public class SectionsMaster
    {
        SectionsMaster()
        {
            CreateOn = DateTime.Now;
        }
        public int Id { get; set; }
        public string SectionName { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
