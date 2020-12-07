

using System;

namespace School.Domain.Report
{
   public class Evaluation
    {
        public Guid studentId { get; set; }
        public int ClassId { get; set; }
        public string Evaluation1 { get; set; }
        public string Evaluation2 { get; set; }
        public string Evaluation3 { get; set; }
    }
}
