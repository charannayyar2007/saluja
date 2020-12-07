
using School.Domain.Report;

namespace Infra.School.Data.Contract
{
  public  interface IEvaluationRepository
    {
        void InsertMarks(Evaluation obj );
        void updateMarks(Evaluation obj);
    }
}
