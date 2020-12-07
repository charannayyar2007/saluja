using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Master
{
   public class HolidayMaster
    {
        public int id { get; set; }
        public Nullable<System.DateTime> HolidayDate { get; set; }
        public Nullable<System.DateTime> createon { get; set; }
    }
}
