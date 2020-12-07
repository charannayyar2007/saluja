using Infra.School.Data.Contract;
using Infra.School.Data.DataFactory;
using Infra.School.Data.ReportCardRepository;
using School.Application.Contract;
using School.Domain.Report;
using System;

namespace School.Application.ReportCardService
{
   public class EvaluationService : IEvaluationService
    {
        private readonly IEvaluationRepository _evaluation;
        public EvaluationService() {
            _evaluation = FactoryDataLayer<EvaluationRepository>.Create();

        }
        public void InsertMarks(Evaluation obj)
        {
            _evaluation.InsertMarks(obj);
        }

        public void updateMarks(Evaluation obj)
        {
            throw new NotImplementedException();
        }
    }
}
