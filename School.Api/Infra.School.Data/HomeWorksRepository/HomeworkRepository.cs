using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Infra.School.Data.AutoMapper;
using Infra.School.Data.Contract;
using Infra.School.Data.Db;
using School.Domain.HomeWork;
using School.Dto.HomeWorks;
using School.Dto.Master;

namespace Infra.School.Data.HomeWorksRepository
{
    public class HomeworkRepository : IHomeWorkRepository
    {
        private readonly IMapper mapper;
        public HomeworkRepository()
        {
            mapper = DbAutoMapperProfiles.Mappers(); ;
        }

        #region Teacher
        public async Task<bool> UploadFile(TeacherAssignment assignment)
        {
            using (var entity = new SchoolErp())
            {
                Guid uniqueId = Guid.Empty;
                var details = from role in entity.UserRoles
                              join user in entity.Users on role.UniqueId equals user.UniqueId
                              where user.userId == assignment.enrollmentCode || user.PhoneNo == assignment.enrollmentCode
                              || user.EmailId == assignment.enrollmentCode
                              select new { role.UniqueId };
                foreach (var v in details)
                {
                    uniqueId = v.UniqueId;
                }
                var sub = entity.SubjectMasters.Where(x => x.id == assignment.subjectId).FirstOrDefault().Subject.Replace(" ", "");
                var cla = entity.ClassMasters.Where(x => x.id == assignment.classid).FirstOrDefault().className.Replace(" ", "");
                var sec = entity.Sections.Where(x => x.id == assignment.sectionId).FirstOrDefault().sectionName.Replace(" ", "");

                if (assignment.uploadedby == 3)
                {
                    assignment.AssId = sub + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + cla + "_" + sec;
                }
                Assigment assig = new Assigment()
                {
                    enrollmentCode = assignment.enrollmentCode,
                    subjectId = assignment.subjectId,
                    fileName = assignment.FileName,
                    filepath = assignment.filepath,
                   deadlineDate = assignment.deadlineDate,
                    uploadeOn = assignment.uploadeOn,
                    classid = assignment.classid,
                    sectionId = assignment.sectionId,
                    uploadedby = assignment.uploadedby,
                    UniqueId = uniqueId,
                    AssID = assignment.AssId,
                    uploadstatus=true
                   // Remark = assignment.Remark
                };
                entity.Assigments.Add(assig);
                var result = await entity.SaveChangesAsync();
                if (result >= 1) {
                
                    //var assUp = new AssignmentUploadStatu()
                    //{
                    //    AssId = assignment.AssId,
                    //    enrollmentCode = assignment.enrollmentCode,
                    //    classId = assignment.classid,
                    //    subjectid = assignment.subjectId,
                    //    UniqueId = uniqueId,
                    //    uploadstatus = true
                    //};
                    //entity.AssignmentUploadStatus.Add(assUp);
                    //await entity.SaveChangesAsync();
                    return true;
                }

                else
                    return false;
            }

        }


        public async Task<bool> EditAssignment(TeacherAssignment assignment)
        {
            using (var entity = new SchoolErp())
            {
                Guid uniqueId = Guid.Empty;

                var sub = entity.SubjectMasters.Where(x => x.id == assignment.subjectId).FirstOrDefault().Subject;
                var cla = entity.ClassMasters.Where(x => x.id == assignment.classid).FirstOrDefault().className;
                var sec = entity.Sections.Where(x => x.id == assignment.sectionId).FirstOrDefault().sectionName;
                var details = await entity.Assigments.Where(x => x.id == assignment.Id).FirstOrDefaultAsync();
                details.subjectId = assignment.subjectId;
                details.fileName = assignment.FileName;
                details.filepath = assignment.filepath;
                details.deadlineDate = assignment.deadlineDate;
                details.uploadeOn = assignment.uploadeOn;
                details.classid = assignment.classid;
                details.sectionId = assignment.sectionId;
                details.AssID = sub + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + cla + "_" + sec;

                var result = await entity.SaveChangesAsync();
                if (result >= 1)
                {
                    //var assUp = await entity.AssignmentUploadStatus.Where(x => x.id == assignment.id).FirstOrDefaultAsync();

                    //assUp.AssId = sub + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + cla + "_" + sec;
                    //assUp.classId = assignment.classid;
                    //assUp.subjectid = assignment.subjectId;
                    //await entity.SaveChangesAsync();
                    return true;
                }

                else
                    return false;
            }

        }
        public async Task<List<TeacherViewAssignment>> ViewAllAssignment(string UserId)
        {
            using (var entity = new SchoolErp())
            {
                var res = await (from stu in entity.Students
                                 join cl in entity.ClassMasters on stu.classId equals cl.id
                                 join sec in entity.Sections on stu.sectionId equals sec.id
                                 join Ass in entity.Assigments on new { a = stu.classId, stu.sectionId } equals new { a = Ass.classid, Ass.sectionId } into gj
                                 from Assign in gj.DefaultIfEmpty()
                                 join sub in entity.SubjectMasters on Assign.subjectId equals sub.id
                                 where Assign.enrollmentCode == UserId
                                 orderby cl.id
                                 select new TeacherViewAssignment() 
                                 {
                                     Id = entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.AssID == Assign.AssID).FirstOrDefault().id != null ? entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.AssID == Assign.AssID).FirstOrDefault().id : 0,
                                     FirstName = stu.firstname,
                                     LastName = stu.lastName,
                                     ClassName = cl.className,
                                     SectionName = sec.sectionName,
                                     Subject = sub.Subject,
                                     DownloadCheck=entity.StudentAssignmentFiles.Where(x=>x.UniqueId==stu.UniqueId && x.AssId==Assign.AssID).Select(x=>x.DownloadCheck).ToList(),
                                     //FileNames = entity.StudentAssignmentFiles.Where(x => x.UniqueId == stu.UniqueId  && x.AssId == Assign.AssID).Select(x=>x.FileName).ToList(),
                                     //  filepath = entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().filepath ?? "-",
                                     uploadOn = entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadeOn != null ? SqlFunctions.DateName("day", entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadeOn) + "-" + SqlFunctions.DatePart("month", entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadeOn) + "-" + SqlFunctions.DateName("year", entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadeOn) : "-",
                                     LastUploadDate = Assign.deadlineDate != null ? SqlFunctions.DateName("day", Assign.deadlineDate) + "-" + SqlFunctions.DatePart("month", Assign.deadlineDate) + "-" + SqlFunctions.DateName("year", Assign.deadlineDate) : "-",
                                     Status = entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadstatus != null ? true : false,
                                     Remarks = entity.StudentAssignmentFiles.Where(x => x.UniqueId == stu.UniqueId && x.AssId == Assign.AssID).Select(x => x.Remark).ToList(),
                                    // Remark = string.Concat(" ", (entity.StudentAssignmentFiles.Where(x => x.UniqueId == stu.UniqueId && x.AssId == Assign.AssID).Select(a => a.Remark).ToList())),
                                     }).ToListAsync();
                
                return res;
            }

        }



        public async Task<List<TeacherUploadAssignment>> ViewAssignmentByTeacher(string UserId)
        {
            using (var entity = new SchoolErp())
            {
                var res = await (from Ass in entity.Assigments
                                 join cla in entity.ClassMasters on Ass.classid equals cla.id
                                 join sect in entity.Sections on Ass.sectionId equals sect.id
                                 join sub in entity.SubjectMasters on Ass.subjectId equals sub.id
                                 where Ass.enrollmentCode == UserId
                                 orderby Ass.id descending
                                 select new TeacherUploadAssignment()
                                 {
                                     Id = Ass.id,
                                     ClassName = cla.className,
                                     SectionName = sect.sectionName,
                                     Subject = sub.Subject,
                                     fileName = Ass.fileName,
                                     filepath = Ass.filepath,
                                     uploadOn = Ass.uploadeOn != null ? SqlFunctions.DateName("day", Ass.uploadeOn) + "/" + SqlFunctions.DatePart("month", Ass.uploadeOn) + "/" + SqlFunctions.DateName("year", Ass.uploadeOn) : "-",
                                     LastUploadDate = Ass.deadlineDate != null ? SqlFunctions.DateName("day", Ass.deadlineDate) + "/" + SqlFunctions.DatePart("month", Ass.deadlineDate) + "/" + SqlFunctions.DateName("year", Ass.deadlineDate) : "-",
                                 }).ToListAsync();
                return res;
            }
        }

        public async Task<List<string>> DeleteHW(int FileId)
        {
            List<string> filepath = new List<string>();
            using (var entity = new SchoolErp())
            {
                var res = await entity.Assigments.Where(x => x.id == FileId).FirstAsync();
                var AssId = res.AssID;
                entity.Assigments.Remove(res);
                var val = await entity.SaveChangesAsync();

                if (val >= 1)
                {
                    var resu = await entity.StudentAssignmentFiles.Where(x => x.AssId == AssId && x.AssignmentId == FileId).ToListAsync();
                    foreach (var v in resu)
                        filepath.Add(v.FilePath);
                    entity.StudentAssignmentFiles.RemoveRange(resu);
                    resu.Clear();
                   await entity.SaveChangesAsync();
                    return filepath;
                }
                return null;
            }
        }

        public async Task<List<TeacherDownloadAssignment>> DownloadHW(int FileId)
        {
            List<TeacherDownloadAssignment> fileDetails = new List<TeacherDownloadAssignment>();
            using (var entity = new SchoolErp())
            {
                var res = await entity.StudentAssignmentFiles.Where(x => x.AssignmentId == FileId).ToListAsync();
                foreach (var v in res)
                {
                    var r = new TeacherDownloadAssignment()
                    {
                        fileName=v.FileName,
                        filepath=v.FilePath,
                        FileId=v.Id
                    };
                    fileDetails.Add(r);
                    
                }
                res.ForEach(a => a.DownloadCheck = true);
              await  entity.SaveChangesAsync();
                
                return fileDetails;
            }
        }

        public async Task<TeacherAssignment> EditHW(int FileId)
        {
            using (var entity = new SchoolErp())
            {
                var res = await entity.Assigments.Where(x => x.id == FileId).FirstAsync();
                var re = new TeacherAssignment
                {
                    Id = res.id,
                    classid = res.classid,
                    subjectId = res.subjectId,
                    sectionId = res.sectionId,
                    uploadeOn = res.uploadeOn,
                    deadlineDate = res.deadlineDate,
                    FileName = res.fileName,
                    filepath = res.filepath
                };
                return re;
            }
        }

        public async Task<List<TeacherUploadAssignment>> SearchHWByTeacher(int ClassId, int SubId, string date, string UserId)
        {
            DateTime? d = null;
            if (date != null)
                d = DateTime.Parse(date);

           
            using (var entity = new SchoolErp())
            {
                if (ClassId != 0 && SubId != 0 && date != null)
                {
                    var res = await (from Ass in entity.Assigments
                                     join cla in entity.ClassMasters on Ass.classid equals cla.id
                                     join sect in entity.Sections on Ass.sectionId equals sect.id
                                     join sub in entity.SubjectMasters on Ass.subjectId equals sub.id
                                     where Ass.enrollmentCode == UserId && (Ass.classid == ClassId && Ass.subjectId == SubId && Ass.uploadeOn.ToString() == date)
                                     orderby Ass.id descending
                                     select new TeacherUploadAssignment()
                                     {
                                         Id = Ass.id,
                                         ClassName = cla.className,
                                         SectionName = sect.sectionName,
                                         Subject = sub.Subject,
                                         fileName = Ass.fileName,
                                         filepath = Ass.filepath,
                                         uploadOn = Ass.uploadeOn != null ? SqlFunctions.DateName("day", Ass.uploadeOn) + "/" + SqlFunctions.DatePart("month", Ass.uploadeOn) + "/" + SqlFunctions.DateName("year", Ass.uploadeOn) : "-",
                                         LastUploadDate = Ass.deadlineDate != null ? SqlFunctions.DateName("day", Ass.deadlineDate) + "/" + SqlFunctions.DatePart("month", Ass.deadlineDate) + "/" + SqlFunctions.DateName("year", Ass.deadlineDate) : "-",
                                     }).ToListAsync();
                    return res;
                }
                else if ((ClassId != 0 && SubId != 0) || (ClassId != 0 && date != null) || (SubId != 0 && date != null))
                {
                    var res = await (from Ass in entity.Assigments
                                     join cla in entity.ClassMasters on Ass.classid equals cla.id
                                     join sect in entity.Sections on Ass.sectionId equals sect.id
                                     join sub in entity.SubjectMasters on Ass.subjectId equals sub.id
                                     where Ass.enrollmentCode == UserId && ((Ass.classid == ClassId && Ass.subjectId == SubId) || (Ass.subjectId == SubId && Ass.uploadeOn.ToString() == date) || (Ass.classid == ClassId && Ass.uploadeOn.ToString() == date))
                                     orderby Ass.id descending
                                     select new TeacherUploadAssignment()
                                     {
                                         Id = Ass.id,
                                         ClassName = cla.className,
                                         SectionName = sect.sectionName,
                                         Subject = sub.Subject,
                                         fileName = Ass.fileName,
                                         filepath = Ass.filepath,
                                         uploadOn = Ass.uploadeOn != null ? SqlFunctions.DateName("day", Ass.uploadeOn) + "/" + SqlFunctions.DatePart("month", Ass.uploadeOn) + "/" + SqlFunctions.DateName("year", Ass.uploadeOn) : "-",
                                         LastUploadDate = Ass.deadlineDate != null ? SqlFunctions.DateName("day", Ass.deadlineDate) + "/" + SqlFunctions.DatePart("month", Ass.deadlineDate) + "/" + SqlFunctions.DateName("year", Ass.deadlineDate) : "-",
                                     }).ToListAsync();
                    return res;
                }
                else
                {
                    var res = await (from Ass in entity.Assigments
                                     join cla in entity.ClassMasters on Ass.classid equals cla.id
                                     join sect in entity.Sections on Ass.sectionId equals sect.id
                                     join sub in entity.SubjectMasters on Ass.subjectId equals sub.id
                                     where Ass.enrollmentCode == UserId && (Ass.classid == ClassId || Ass.subjectId == SubId ||Ass.uploadeOn.ToString()==date)
                                     orderby Ass.id descending
                                     select new TeacherUploadAssignment()
                                     {
                                         Id = Ass.id,
                                         ClassName = cla.className,
                                         SectionName = sect.sectionName,
                                         Subject = sub.Subject,
                                         fileName = Ass.fileName,
                                         filepath = Ass.filepath,
                                         uploadOn = Ass.uploadeOn != null ? SqlFunctions.DateName("day", Ass.uploadeOn) + "/" + SqlFunctions.DatePart("month", Ass.uploadeOn) + "/" + SqlFunctions.DateName("year", Ass.uploadeOn) : "-",
                                         LastUploadDate = Ass.deadlineDate != null ? SqlFunctions.DateName("day", Ass.deadlineDate) + "/" + SqlFunctions.DatePart("month", Ass.deadlineDate) + "/" + SqlFunctions.DateName("year", Ass.deadlineDate) : "-",
                                     }).ToListAsync();
                    return res;
                }

            }

        }

        public async Task<List<TeacherViewAssignment>> SearchViewHW(int ClassId, int SubId, string date, string UserId)
        {

            using (var entity = new SchoolErp())
            {
                if (ClassId != 0 && SubId != 0 && date != null)
                {

                    var res = await (from stu in entity.Students
                                     join cl in entity.ClassMasters on stu.classId equals cl.id
                                     join sec in entity.Sections on stu.sectionId equals sec.id
                                     join Ass in entity.Assigments on new { a = stu.classId, stu.sectionId } equals new { a = Ass.classid, Ass.sectionId } into gj
                                     from Assign in gj.DefaultIfEmpty()
                                     join sub in entity.SubjectMasters on Assign.subjectId equals sub.id
                                     where Assign.enrollmentCode == UserId && (Assign.classid == ClassId && Assign.subjectId == SubId && Assign.deadlineDate.ToString() == date)
                                     orderby cl.id
                                     select new TeacherViewAssignment()
                                     {
                                         Id = entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.AssID == Assign.AssID).FirstOrDefault().id != null ? entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.AssID == Assign.AssID).FirstOrDefault().id : 0,
                                         FirstName = stu.firstname,
                                         LastName = stu.lastName,
                                         ClassName = cl.className,
                                         SectionName = sec.sectionName,
                                         Subject = sub.Subject,
                                         DownloadCheck = entity.StudentAssignmentFiles.Where(x => x.UniqueId == stu.UniqueId && x.AssId == Assign.AssID).Select(x => x.DownloadCheck).ToList(),
                                         //FileNames = entity.StudentAssignmentFiles.Where(x => x.UniqueId == stu.UniqueId  && x.AssId == Assign.AssID).Select(x=>x.FileName).ToList(),
                                         //  filepath = entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().filepath ?? "-",
                                         uploadOn = entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadeOn != null ? SqlFunctions.DateName("day", entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadeOn) + "-" + SqlFunctions.DatePart("month", entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadeOn) + "-" + SqlFunctions.DateName("year", entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadeOn) : "-",
                                         LastUploadDate = Assign.deadlineDate != null ? SqlFunctions.DateName("day", Assign.deadlineDate) + "-" + SqlFunctions.DatePart("month", Assign.deadlineDate) + "-" + SqlFunctions.DateName("year", Assign.deadlineDate) : "-",
                                         Status = entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadstatus != null ? true : false,
                                         Remarks = entity.StudentAssignmentFiles.Where(x => x.UniqueId == stu.UniqueId && x.AssId == Assign.AssID).Select(x => x.Remark).ToList(),
                                     }).ToListAsync();

                    return res;
                }
                else if ((ClassId != 0 && SubId != 0) || (ClassId != 0 && date != null) || (SubId != 0 && date != null))
                {
                    var res = await (from stu in entity.Students
                                     join cl in entity.ClassMasters on stu.classId equals cl.id
                                     join sec in entity.Sections on stu.sectionId equals sec.id
                                     join Ass in entity.Assigments on new { a = stu.classId, stu.sectionId } equals new { a = Ass.classid, Ass.sectionId } into gj
                                     from Assign in gj.DefaultIfEmpty()
                                     join sub in entity.SubjectMasters on Assign.subjectId equals sub.id
                                     where Assign.enrollmentCode == UserId && ((Assign.classid == ClassId && Assign.subjectId == SubId) || (Assign.subjectId == SubId && Assign.deadlineDate.ToString() == date) || (Assign.classid == ClassId && Assign.deadlineDate.ToString() == date))
                                     orderby cl.id
                                     select new TeacherViewAssignment()
                                     {
                                         Id = entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.AssID == Assign.AssID).FirstOrDefault().id != null ? entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.AssID == Assign.AssID).FirstOrDefault().id : 0,
                                         FirstName = stu.firstname,
                                         LastName = stu.lastName,
                                         ClassName = cl.className,
                                         SectionName = sec.sectionName,
                                         Subject = sub.Subject,
                                         DownloadCheck = entity.StudentAssignmentFiles.Where(x => x.UniqueId == stu.UniqueId && x.AssId == Assign.AssID).Select(x => x.DownloadCheck).ToList(),
                                         //FileNames = entity.StudentAssignmentFiles.Where(x => x.UniqueId == stu.UniqueId  && x.AssId == Assign.AssID).Select(x=>x.FileName).ToList(),
                                         //  filepath = entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().filepath ?? "-",
                                         uploadOn = entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadeOn != null ? SqlFunctions.DateName("day", entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadeOn) + "-" + SqlFunctions.DatePart("month", entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadeOn) + "-" + SqlFunctions.DateName("year", entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadeOn) : "-",
                                         LastUploadDate = Assign.deadlineDate != null ? SqlFunctions.DateName("day", Assign.deadlineDate) + "-" + SqlFunctions.DatePart("month", Assign.deadlineDate) + "-" + SqlFunctions.DateName("year", Assign.deadlineDate) : "-",
                                         Status = entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadstatus != null ? true : false,
                                         Remarks = entity.StudentAssignmentFiles.Where(x => x.UniqueId == stu.UniqueId && x.AssId == Assign.AssID).Select(x => x.Remark).ToList(),
                                     }).ToListAsync();

                    return res;
                }
                else
                {
                    var res = await (from stu in entity.Students
                                     join cl in entity.ClassMasters on stu.classId equals cl.id
                                     join sec in entity.Sections on stu.sectionId equals sec.id
                                     join Ass in entity.Assigments on new { a = stu.classId, stu.sectionId } equals new { a = Ass.classid, Ass.sectionId } into gj
                                     from Assign in gj.DefaultIfEmpty()
                                     join sub in entity.SubjectMasters on Assign.subjectId equals sub.id
                                     where Assign.enrollmentCode == UserId && (Assign.classid == ClassId || Assign.subjectId == SubId || Assign.deadlineDate.ToString() == date)
                                     orderby cl.id
                                     select new TeacherViewAssignment()
                                     {
                                         Id = entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.AssID == Assign.AssID).FirstOrDefault().id != null ? entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.AssID == Assign.AssID).FirstOrDefault().id : 0,
                                         FirstName = stu.firstname,
                                         LastName = stu.lastName,
                                         ClassName = cl.className,
                                         SectionName = sec.sectionName,
                                         Subject = sub.Subject,
                                         DownloadCheck = entity.StudentAssignmentFiles.Where(x => x.UniqueId == stu.UniqueId && x.AssId == Assign.AssID).Select(x => x.DownloadCheck).ToList(),
                                         //FileNames = entity.StudentAssignmentFiles.Where(x => x.UniqueId == stu.UniqueId  && x.AssId == Assign.AssID).Select(x=>x.FileName).ToList(),
                                         //  filepath = entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().filepath ?? "-",
                                         uploadOn = entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadeOn != null ? SqlFunctions.DateName("day", entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadeOn) + "-" + SqlFunctions.DatePart("month", entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadeOn) + "-" + SqlFunctions.DateName("year", entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadeOn) : "-",
                                         LastUploadDate = Assign.deadlineDate != null ? SqlFunctions.DateName("day", Assign.deadlineDate) + "-" + SqlFunctions.DatePart("month", Assign.deadlineDate) + "-" + SqlFunctions.DateName("year", Assign.deadlineDate) : "-",
                                         Status = entity.Assigments.Where(x => x.UniqueId == stu.UniqueId && x.subjectId == sub.id && x.AssID == Assign.AssID).FirstOrDefault().uploadstatus != null ? true : false,
                                         Remarks = entity.StudentAssignmentFiles.Where(x => x.UniqueId == stu.UniqueId && x.AssId == Assign.AssID).Select(x => x.Remark).ToList(),
                                     }).ToListAsync();

                    return res;
                }
            }


        }

        //public async Task<List<ViewsAssignment>> ViewAssignmentbyStudent(string UserId)
        //{
        //    using (var entity = new SchoolErp())
        //    {
        //        var res = await (from Ass in entity.Assigments
        //                         join cla in entity.ClassMasters on Ass.classid equals cla.id
        //                         join sect in entity.Sections on Ass.sectionId equals sect.id
        //                         join sub in entity.SubjectMasters on Ass.subjectId equals sub.id
        //                         join user in entity.Users on Ass.enrollmentCode equals user.userId
        //                         join stu in entity.TeacherDetails on user.userId equals stu.Phone
        //                         join assup in entity.AssignmentUploadStatus on Ass.id equals assup.id
        //                         join s in entity.Students on new { a = Ass.classid, Ass.sectionId } equals new { a = s.classId, s.sectionId }
        //                         where Ass.uploadedby == 3 && (s.phone == UserId)
        //                         select new ViewsAssignment()
        //                         {
        //                             Id = Ass.id,
        //                             firstName = stu.firstName,
        //                             Lastname = stu.Lastname,
        //                             ClassName = cla.className,
        //                             SectionName = sect.sectionName,
        //                             Subject = sub.Subject,
        //                             fileName = Ass.fileName,
        //                             filepath = Ass.filepath,
        //                             uploadOn = Ass.uploadeOn != null ? SqlFunctions.DatePart("month", Ass.uploadeOn) + "/" + SqlFunctions.DateName("day", Ass.uploadeOn) + "/" + SqlFunctions.DateName("year", Ass.uploadeOn) : null,
        //                             LastUploadDate = Ass.deadlineDate != null ? SqlFunctions.DatePart("month", Ass.deadlineDate) + "/" + SqlFunctions.DateName("day", Ass.deadlineDate) + "/" + SqlFunctions.DateName("year", Ass.deadlineDate) : null,
        //                             uploadstatus = entity.AssignmentUploadStatus.Where(x => x.subjectid == Ass.subjectId && x.classId == Ass.classid && x.enrollmentCode == UserId).FirstOrDefault().uploadstatus != null ? true : false,
        //                             AssId = Ass.AssID,
        //                             SubjectId = Ass.subjectId

        //                         }).ToListAsync();
        //        return res;
        //    }

        //}

        #endregion

        #region Student

        public async Task<List<ViewStudentAssignent>> ViewStudentAssignment(string UserId)
        {
            using (var entity = new SchoolErp())
            {
                var res = await (from stu in entity.Students
                                 join user in entity.Users on stu.UniqueId equals user.UniqueId
                                 join ass in entity.Assigments on new { a = stu.classId, stu.sectionId } equals new { a = ass.classid, ass.sectionId }
                                 join sub in entity.SubjectMasters on ass.subjectId equals sub.id
                                 join teacher in entity.TeacherDetails on ass.UniqueId equals teacher.UniqueId
                                 where user.userId == UserId && ass.uploadedby == 3
                                 select new ViewStudentAssignent()
                                 {
                                     AssId = ass.AssID,
                                     Subject = sub.Subject,
                                     SubjectId = sub.id,
                                     FilePath = ass.filepath,
                                     Date = ass.uploadeOn,
                                     LastSubmissionDate = ass.deadlineDate,
                                     AssignedBy = teacher.firstName + " " + teacher.Lastname,
                                     Status = (entity.Assigments.Where(x => x.enrollmentCode == stu.admissionId).FirstOrDefault().uploadstatus !=null?true:false)
                                 }).ToListAsync();
                return res;

            }
        }

        public async Task<ViewStudentAssignent> ViewAssignmentByAssId(string AssId,string AdmissionId)
        {
            using (var entity = new SchoolErp())
            {
                bool uploadstatus = false;
                var status =  (from asup in entity.Assigments
                                    join stu in entity.Students on asup.UniqueId equals stu.UniqueId
                                    where stu.admissionId == AdmissionId && asup.AssID == AssId
                                    select new { asup.uploadstatus }).FirstOrDefault();
                if(status!=null)
                {
                    uploadstatus = true;
                }
                                   

                var res =   await (from ass in entity.Assigments
                           join sub in entity.SubjectMasters on ass.subjectId equals sub.id
                           join tea in entity.TeacherDetails on ass.UniqueId equals tea.UniqueId
                           where ass.AssID == AssId
                           select new { ass.AssID,ass.filepath, ass.deadlineDate, TeacherName=tea.firstName+" "+tea.Lastname, sub.Subject,ass.uploadeOn }).FirstAsync();
                
                var result = new ViewStudentAssignent()
                {
                    AssId =res.AssID,
                    Subject = res.Subject,
                    AssignedBy = res.TeacherName,
                   FilePath = res.filepath,
                    LastSubmissionDate = res.deadlineDate,   
                    Date=res.uploadeOn,
                   Status = uploadstatus


                };
                return result;

                         
            }
        }

        public async Task<bool> StudentUploadHW(StudentUploadAssignment assignment)
        {
            using (var entity = new SchoolErp())
            {
                var assing =await entity.Assigments.Where(x => x.enrollmentCode == assignment.enrollmentCode && x.AssID == assignment.AssId).FirstOrDefaultAsync();
                var res = await entity.Students.Where(x => x.admissionId == assignment.enrollmentCode).FirstOrDefaultAsync();

                if (assing == null)
                {
                    Assigment assign = new Assigment()
                    {
                        AssID = assignment.AssId,
                        classid = assignment.classid,
                        sectionId = assignment.sectionId,
                        subjectId = assignment.subjectId,
                        enrollmentCode = assignment.enrollmentCode,
                        UniqueId = res.UniqueId,
                        uploadedby = 4,
                        uploadeOn = DateTime.Now,
                        uploadstatus=true
                      //  fileName = assignment.FileName.Trim(),
                        //filepath = assignment.filepath,
                        //Remark = assignment.Remark
                    };
                    entity.Assigments.Add(assign);
                    var result = await entity.SaveChangesAsync();
                    if (result >= 1)
                    {

                        //var assUp = new AssignmentUploadStatu()
                        //{
                        //    AssId = assignment.AssId,
                        //    enrollmentCode = assignment.enrollmentCode,
                        //    classId = assignment.classid,
                        //    subjectid = assignment.subjectId,
                        //    UniqueId = res.UniqueId,
                        //    uploadstatus = true
                        //};
                        //entity.AssignmentUploadStatus.Add(assUp);
                        //var resul=await entity.SaveChangesAsync();
                        //if(resul>=1)
                        //{
                            var stuassfile = new StudentAssignmentFile()
                            {
                                AssId = assignment.AssId,
                                UniqueId = res.UniqueId,
                                FileName = assignment.FileName,
                                FilePath = assignment.filepath,
                                Remark = assignment.Remark,
                                AssignmentId=assign.id,
                                DownloadCheck=false
                            };
                            entity.StudentAssignmentFiles.Add(stuassfile);
                            await entity.SaveChangesAsync();

                        }
                        return true;
                   // }
                }
                else
                {
                    // assing.filepath = string.IsNullOrEmpty(assing.filepath) ? assignment.filepath : assing.filepath +","+ assignment.filepath;
                    //assing.fileName = string.IsNullOrEmpty(assing.fileName) ? assignment.FileName.Trim() : assing.fileName.Trim() + "," +assignment.FileName.Trim();
                    //  assing.fileName = assing.fileName + "," + assignment.FileName;
                    // assing.filepath = filepath;
                    //assing.Remark = string.IsNullOrEmpty(assignment.Remark) ? assignment.Remark : assing.Remark + "," + assignment.Remark;
                    var stuassfile = new StudentAssignmentFile()
                    {
                        AssId = assignment.AssId,
                        UniqueId = res.UniqueId,
                        FileName = assignment.FileName,
                        FilePath = assignment.filepath,
                        Remark = assignment.Remark,
                        AssignmentId=assing.id,
                        DownloadCheck=false
                    };
                    entity.StudentAssignmentFiles.Add(stuassfile);
                    await entity.SaveChangesAsync();
                    return true;
                }

                return false;
            }
          
        }

        public async Task<List<ViewFileNameList>> GetUploadedFileName(string AssId,string UserId)
        {
          
            using (var entity = new SchoolErp())
            {
                //var res = await (from Assig in entity.Assigments
                //           join user in entity.Users on Assig.UniqueId equals user.UniqueId
                //           where Assig.AssID == AssId && user.userId == UserId
                //           select new { Assig.fileName }).FirstOrDefaultAsync();

                var res = await (from Assig in entity.StudentAssignmentFiles
                                 join user in entity.Users on Assig.UniqueId equals user.UniqueId
                                 where Assig.AssId == AssId && user.userId == UserId
                                 select new ViewFileNameList()
                                 { FileName= Assig.FileName,FileId=Assig.Id }).ToListAsync();

                if (res != null)
                {
                    return res;
                }
                return null;
            }
        }
        #endregion
    }
}

