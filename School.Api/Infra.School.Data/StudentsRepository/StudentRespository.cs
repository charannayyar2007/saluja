using AutoMapper;
using Infra.School.Data.AutoMapper;
using Infra.School.Data.Contract;
using Infra.School.Data.Db;
using School.Domain.Student;
using School.Dto.Students;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.School.Data.StudentsRepository
{
   public class StudentRespository : IStudentRepository
    {
        private readonly IMapper mapper;
        public StudentRespository()
        {
            mapper = DbAutoMapperProfiles.Mappers(); ;
        }
        public async Task<StudentDetail> AddStudent(StudentDetail studentDetail)
        {
           
                using (var context = new SchoolErp())
                {
                    var studentToadd = new Student()
                    {
                        firstname = studentDetail.firstname,
                        lastName = studentDetail.lastName,
                        admissionId = studentDetail.admissionId,
                        classId = studentDetail.classId,
                        email = studentDetail.email,
                        fathername = studentDetail.fathername,
                        gender = studentDetail.gender,
                        motherName = studentDetail.motherName,
                        sectionId = studentDetail.sectionId,
                        phone = studentDetail.phone,
                        rollNo = studentDetail.rollNo,
                        UniqueId=studentDetail.UniqueId,
                        Address1=studentDetail.Address,
                        DOB=Convert.ToDateTime(studentDetail.DOB)
                    };
                    context.Students.Add(studentToadd);
                    var IsCreated = await context.SaveChangesAsync();
                    if (IsCreated >= 1)
                    {
                        return studentDetail;

                    }
                    return null;
                }
           
        }

    

        public async Task<bool> IsAdmissionIdExist(string id)
        {
            using (var entity = new SchoolErp())
            {
                var user = await entity.Students.Where(x => x.admissionId == id).FirstOrDefaultAsync(); 
                if (user != null)
                {

                    return true;
                }

                return false;
            }
        }

        public async Task<StudentDashboardDetails> GetStudentDetails(string UserId)
        {
              using (var entity = new SchoolErp())
            {
                var user = await (from std in entity.Students
                            join usr in entity.Users on std.UniqueId equals usr.UniqueId
                            join cla in entity.ClassMasters on std.classId equals cla.id
                            join sec in entity.Sections on std.sectionId equals sec.id

                            where usr.userId == UserId || usr.PhoneNo == UserId || usr.EmailId == UserId
                            select new {std.classId,std.sectionId, cla.className, sec.sectionName, std.lastName, std.firstname, std.admissionId,std.rollNo,std.DOB,std.Address1,std.email,std.fathername,std.motherName, std.phone}).FirstOrDefaultAsync();
                 var result=new StudentDashboardDetails()
                    {
                        AdmissionId = user.admissionId,
                        ClassName = user.className,
                        Name = user.firstname,
                        LastName= user.lastName,
                        SectionName = user.sectionName,
                        RollNumber=user.rollNo,
                        ClassId=user.classId,
                        SectionId=user.sectionId,
                        Dob=user.DOB,
                        Adress=user.Address1,
                        Email=user.email,
                        Mobile=user.phone,
                        FatherName=user.fathername

                    };
                  return result;

            }
        }
        public async Task<List<StudentDetail>> GetStudentList(string id)
        {
         
            using (var entity = new SchoolErp())
            {
                var user =await entity.Students.Where(x => x.admissionId.StartsWith(id)).ToListAsync();
                    return mapper.Map<List<StudentDetail>>(user);
            }
        }
        public async Task<List<StudentDetail>> GetStudentList(int classId, int sectionId,string Name)
        {

            using (var entity = new SchoolErp())
            {
                var user = await entity.Students.Where(x => x.classId==classId && x.sectionId==sectionId && x.firstname.StartsWith(Name)).ToListAsync();
                return mapper.Map<List<StudentDetail>>(user);
            }
        }

        public async Task<List<StudentDetail>> GetAllStudent(int start, int end)
        {

            using (var entity = new SchoolErp())
            {
                var user = await (from std in entity.Students
                                  join cla in entity.ClassMasters on std.classId equals cla.id
                                  join sec in entity.Sections on std.sectionId equals sec.id
                                orderby std.Id ascending
                                  select new StudentDetail()
                                  {
                                      ClassName = cla.className,
                                      Section = sec.sectionName,
                                      lastName = std.lastName,
                                      firstname = std.firstname,
                                      admissionId = std.admissionId,
                                      rollNo = std.rollNo,
                                      gender = std.gender,
                                      fathername = std.fathername,
                                     motherName= std.motherName,
                                     DOB =std.DOB!=null ? SqlFunctions.DateName("day", std.DOB) + "/" + SqlFunctions.DatePart("month", std.DOB) + "/" + SqlFunctions.DateName("year", std.DOB)
                                   :null
                                  }).ToListAsync();
                return user;
            }
        }
        public async Task<List<StudentDetail>> GetAllStudent()
        {

            using (var entity = new SchoolErp())
            {
                var user = await (from std in entity.Students
                                  join cla in entity.ClassMasters on std.classId equals cla.id
                                  join sec in entity.Sections on std.sectionId equals sec.id
                                  orderby std.Id ascending
                                  select new StudentDetail()
                                  {
                                      ClassName = cla.className,
                                      Section = sec.sectionName,
                                      lastName = std.lastName,
                                      firstname = std.firstname,
                                      admissionId = std.admissionId,
                                      rollNo = std.rollNo,
                                      gender = std.gender,
                                      fathername = std.fathername,
                                      motherName = std.motherName,
                                      DOB = std.DOB != null ? SqlFunctions.DateName("day", std.DOB) + "/" + SqlFunctions.DatePart("month", std.DOB) + "/" + SqlFunctions.DateName("year", std.DOB)
                                   : null
                                  }).ToListAsync();
                return user;
            }
        }
    }
}
