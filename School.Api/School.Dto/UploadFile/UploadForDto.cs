using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace School.Dto.UploadFile
{
   public class UploadForDto
    {
        public string Name { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}
