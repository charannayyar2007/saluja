using School.Application.Contract;
using School.Application.FactoryApplicationService;
using School.Application.ReportCardService;
using School.Domain.Report;
using System.Threading.Tasks;
using System.Web.Http;

namespace School.Api.Controllers.api
{
    public class ReportCardController : ApiController
    {
        private readonly IEvaluationService _evaluationervice;
        public ReportCardController()
        {
            _evaluationervice = ApplicatonServiceFactory<EvaluationService>.Create();

        }
        [HttpPost]
        [Route("insertEvaluationMarks")]
        public IHttpActionResult MarkAttendace([FromBody] Evaluation evalution)
        {
            if (ModelState.IsValid)
            {
                _evaluationervice.InsertMarks(evalution);
                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}
