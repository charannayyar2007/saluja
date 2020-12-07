
using Infra.School.Data.AdoCore;
using Infra.School.Data.Contract;
using Infra.School.Data.Db;
using School.Domain;
using School.Domain.Attendance;
using School.Domain.Student;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.School.Data.AttendancesRepository
{
    public class AttendanceRepository : TemplateADO<DataTable>, IRepository<DataTable>, IAttendanceRepository
    {
  
        public override void Add(DataTable obj)
        {
            base.Add(obj);
        }
        public async Task<MarkAttendace> MarkeAttence(string admission, string presentstatus)
        {
            MarkAttendace markAtts= new MarkAttendace();
           using (var entity= new SchoolErp())
            {
                var student = entity.Attendances.Where(x => DbFunctions.TruncateTime(x.punchdate)== DbFunctions.TruncateTime(DateTime.Now)&& x.EnrollmentCode == admission).FirstOrDefault();
                
                if (presentstatus == "P" && student!=null)
                {
                 
                    entity.Attendances.Remove(student);
                  await  entity.SaveChangesAsync();
                    var markAtt = (from a in entity.Students
                                   join w in entity.ClassMasters on a.classId equals w.id
                                   join t in entity.Sections on a.sectionId equals t.id
                                   where a.admissionId == admission
                                   select new { Name = a.firstname + " " + a.lastName, 
                                       ClassName = w.className, Section = t.sectionName,
                                       AdmissionId = a.admissionId, 
                                       Date = DateTime.Now }).FirstOrDefault();
                    markAtts.AdmissionId = markAtt.AdmissionId;
                    markAtts.Name = markAtt.Name;
                    markAtts.ClassName = markAtt.ClassName;
                    markAtts.Section = markAtt.Section;
                    markAtts.Date = markAtt.Date.ToString("dd/MM/yyy");
                    markAtts.PresentStatus = "P";
                    return markAtts;
                }
                if (presentstatus == "A"&&student == null)
                {
                    Attendance obj = new Attendance();
                    obj.attendanceId = 1;
                    obj.EnrollmentCode = admission;
                    obj.markedby = 3;
                    obj.punchdate = DateTime.Now;
                    entity.Attendances.Add(obj);
                    await entity.SaveChangesAsync();
                    var markAtt = (from a in entity.Students
                                   join w in entity.ClassMasters on a.classId equals w.id
                                   join t in entity.Sections on a.sectionId equals t.id
                                  where a.admissionId==admission
                                   select new { Name = a.firstname + " " + a.lastName, ClassName = w.className, Section = t.sectionName, AdmissionId = a.admissionId, Date = DateTime.Now}).FirstOrDefault();
                    markAtts.AdmissionId = markAtt.AdmissionId;
                    markAtts.Name = markAtt.Name;
                    markAtts.ClassName = markAtt.ClassName;
                    markAtts.Section = markAtt.Section;
                    markAtts.Date = markAtt.Date.ToString("dd/MM/yyy");
                    markAtts.PresentStatus = "A";
                    return markAtts;
                }
                markAtts.ErrorMsg = "already Mark";
                return markAtts;
            }
        }
        public async Task GetAttendaceByClass(int classId, int sectionId)
        {
            using (var entity = new SchoolErp())
            {
                //entity.proc_vi
           

                var students =await (from s in entity.Students
                                where s.classId == classId && s.sectionId == sectionId
                                select new ClassWiseAttenceReport()
                                {
                                    ClassId = s.classId,
                                    RollNo = s.rollNo,
                                    AdmissionId = s.admissionId,
                                    Date = DateTime.Now.Date,
                                    PresentStatus = "P",
                                    StudentName = s.firstname + " " + s.lastName,
                                    SectionId = s.sectionId
                                }).ToListAsync();

               
                var update = from s in students
                             join att in entity.Attendances
                              on s.AdmissionId equals att.EnrollmentCode
                              into gj
                             from x in gj.DefaultIfEmpty()
                             where DbFunctions.TruncateTime(x.punchdate) ==DateTime.Now.Date
                             select new {s.StudentName,x.EnrollmentCode };


               //await entity.Students.Where(x => x.classId == classId && x.sectionId == sectionId).ToListAsync();
               
            }
        }
  

        protected override void ExecuteCommand(DataTable obj)
        {
            throw new System.NotImplementedException();
        }

        protected override List<DataTable> ExecuteCommand()
        {
            throw new NotImplementedException();


        }

        protected override List<DataTable> ExecuteCommand(string AdmissionId ,int? classId, int? sectionid, int? year, int? month)
        {
            
            List<DataTable> ob = new List<DataTable>();
            if (AdmissionId != null && year!=null && month!=null) {
                objCommand.CommandText = "Proc_GetAttendaceByAdmissionId";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@year", year);
                objCommand.Parameters.AddWithValue("@month", month);
                objCommand.Parameters.AddWithValue("@admissionId", AdmissionId);
                using (SqlDataReader sqlDataReader = objCommand.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(sqlDataReader);
                    ob.Add(dt);

                    return ob;

                }
            }
            if (classId != null && sectionid!= null && year==null) {
                objCommand.CommandText = "Proc_ViewAttendaceDatewise";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@classId", classId);
                objCommand.Parameters.AddWithValue("@sectionid", sectionid);
      
                using (SqlDataReader sqlDataReader = objCommand.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(sqlDataReader);
                    ob.Add(dt);
                    return ob;


                }
            }
            if (year != null && month != null)
            {
                objCommand.CommandText = "Proc_GetAttendaceReport";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@classId", classId);
                objCommand.Parameters.AddWithValue("@sectionid", sectionid);
                objCommand.Parameters.AddWithValue("@year", year);
                objCommand.Parameters.AddWithValue("@month", month);
                using (SqlDataReader sqlDataReader = objCommand.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(sqlDataReader);
                    ob.Add(dt);

                    return ob;

                }
            }
            return ob;
         
        }
    }
}
