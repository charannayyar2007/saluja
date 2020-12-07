

using Infra.School.Data.Contract;
using Infra.School.Data.Db;
using School.Domain.Report;

namespace Infra.School.Data.ReportCardRepository
{
   public class EvaluationRepository : IEvaluationRepository
    {
        public void InsertMarks(Evaluation obj)
        {
            using (var context = new SchoolErp())
            {
            }
        }

            public void updateMarks(Evaluation obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
