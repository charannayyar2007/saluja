using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Master
{
   public class ClassesMaster
    {
        public ClassesMaster()
        {
            CreateOn = DateTime.Now;
        }
        public int Id { get; set; }
        public string ClassName { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
