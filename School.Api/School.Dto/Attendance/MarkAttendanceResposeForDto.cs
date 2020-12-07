

using System;

namespace School.Dto.Attendance
{
   public class MarkAttendanceResposeForDto
    {
        public MarkAttendanceResposeForDto()
        {
            Date= DateTime.Now.ToString("dd'/'MM'/'yyyy");

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
