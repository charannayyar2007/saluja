using School.Domain.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Contract
{
    public interface IResultService
    {
        List<SubjectsMaster> GetSubjectList();
        dynamic ExportExcel(int? classId = null);
        List<DataTable> ReportCard(string admissionValue);
        string ResultBulk(DataTable dt);
        DataTable ViewStudent(int classId,int sectionId, int sessionId, string admissionId, string name);
    }
}
