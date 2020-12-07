using System;

namespace School.Domain.Attendance
{
public class MarkAttendace
    {
        public MarkAttendace()
        {
            Date = DateTime.Now.ToString("dd'/'MM'/'yyyy");

        }
        public string AdmissionId { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string Section { get; set; }
        public string Date { get; set; }
        public string ErrorMsg { get; set; }
        public string PresentStatus { get; set; }
    }
}
