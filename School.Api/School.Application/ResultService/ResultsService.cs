using Infra.School.Data.Contract;
using Infra.School.Data.DataFactory;
using Infra.School.Data.ResultRepository;
using School.Application.Contract;
using School.Domain.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.ResultService
{
    public class ResultsService : BaseService, IResultService
    {
        private readonly IResultsRepository resultRepository;
        private readonly IRepository<DataTable> _repository;

        public ResultsService()
        {
            _repository = FactoryDataLayer<ResultsRepository>.Create();
            resultRepository = FactoryDataLayer<ResultsRepository>.Create();
        }

        public List<SubjectsMaster> GetSubjectList()
        {
            return resultRepository.GetSubjectList();
        }

        public dynamic ExportExcel(int? classId = null)
        {
            return _repository.GetAttView(null, classId, null, null, null);
        }

        public List<DataTable> ReportCard(string admissionValue)
        {
            return resultRepository.ReportCards(admissionValue);
        }

        public string ResultBulk(DataTable dt)
        {
            return resultRepository.ResultBulkInsert(dt);
        }

        public DataTable ViewStudent(int classId, int sectionId, int sessionId, string admissionId, string name)
        {
            return resultRepository.ViewStudent(classId,sectionId,sessionId,admissionId,name);
        }

    }
}
