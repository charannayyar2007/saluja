using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Dto.Master
{
    public class SessionMasterForDto
    {
        public int id { get; set; }
        public int? starthrs { get; set; }
        public int? startmin { get; set; }
        public int? endHrs { get; set; }
        public int? endMin { get; set; }
        public string startTimeformate { get; set; }
        public string EndTimeformate { get; set; }
        public string sessionYear { get; set; }
        public int? Years { get; set; }
        public int? Months { get; set; }
        public int? EndMonths { get; set; }
    }
}
