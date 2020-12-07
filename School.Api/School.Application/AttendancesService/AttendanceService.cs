using Infra.School.Data.AttendancesRepository;
using Infra.School.Data.Contract;
using Infra.School.Data.DataFactory;
using School.Application.Contract;
using School.Domain;
using School.Dto.Attendance;
using System;
using System.Data;
using System.Threading.Tasks;

namespace School.Application.AttendancesService
{
    public class AttendanceService :BaseService, IAttendanceService
    {
        private readonly IAttendanceRepository _attendaceRepository;
        private readonly IRepository<DataTable> _repository;
        public AttendanceService()
        {
            _repository =FactoryDataLayer<AttendanceRepository>.Create();
            _attendaceRepository = FactoryDataLayer<AttendanceRepository>.Create();
        }
        public  dynamic ViewAttendance( int? classId=null, int? sectionId=null, int? month=null, string sessionYear=null)
        {
        
            int? year=null;
            string[] years;
            if (sessionYear != null)
            {
                 years = sessionYear.Split('-');
                if (month >= 3)
                    year = Convert.ToInt32(years[0]);
                else
                    year = Convert.ToInt32(years[1]);
            }
            return _repository.GetAttView(null, classId, sectionId, year, month );
         
        }
      

        public async Task<MarkAttendanceResposeForDto> MarkAttendance(string AdmissionId, string Pstatus)
        {
            var res = await _attendaceRepository.MarkeAttence(AdmissionId, Pstatus);
        return     mapper.Map<MarkAttendanceResposeForDto>(res);
                    }

        public dynamic ViewAttendance(string AdmissionId, int month, int sessionYear)
        {
            return _repository.GetAttView(AdmissionId, null, null,sessionYear, month);
        }

        public dynamic ViewAttendance(int classId, int sectionId)
        {
            return _repository.GetAttView(null,classId, sectionId, null, null);
        }
    }
}
