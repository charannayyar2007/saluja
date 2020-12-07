using School.Domain.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.School.Data.Contract
{
    public interface IResultsRepository
    {
        List<SubjectsMaster> GetSubjectList();
        string ResultBulkInsert(DataTable dt);
        List<DataTable> ReportCards(string admissionValue);
        DataTable ViewStudent(int classId, int sectionId, int sessionId, string admissionId, string name);
    }
}
