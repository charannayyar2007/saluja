using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.HomeWork
{
    public class ViewAssignment
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string Subject { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string UploadDate { get; set; }
        public string LastSubmissionDate { get; set; }
        public bool? Status { get; set; }
    }
}
