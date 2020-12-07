

using School.Domain.Report;

namespace School.Application.Contract
{
   public interface IEvaluationService
    {
        void InsertMarks(Evaluation obj);
        void updateMarks(Evaluation obj);
    }
}
