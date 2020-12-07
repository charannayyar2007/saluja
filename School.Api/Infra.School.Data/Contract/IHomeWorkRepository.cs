
using Infra.School.Data.Db;
using School.Domain.HomeWork;
using School.Dto.HomeWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infra.School.Data.Contract
{
    public interface IHomeWorkRepository
    {
        #region Teacher
        Task<bool> UploadFile(TeacherAssignment assignment);
        
        Task<bool> EditAssignment(TeacherAssignment assignment);
        Task<List<TeacherViewAssignment>> ViewAllAssignment(string UserId);

        //Task<List<ViewsAssignment>> ViewAssignmentbyStudent(string UserId);
        Task<List<TeacherUploadAssignment>> ViewAssignmentByTeacher(string UserId);

        Task<List<string>> DeleteHW(int FileId);
        Task<List<TeacherDownloadAssignment>> DownloadHW(int FileId);
        Task<TeacherAssignment> EditHW(int FileId);

        Task<List<TeacherUploadAssignment>> SearchHWByTeacher(int ClassId, int SubId, string date, string UserId);

        Task<List<TeacherViewAssignment>> SearchViewHW(int ClassId, int SubId, string date, string UserId);
        #endregion

        #region Student
        Task<List<ViewStudentAssignent>> ViewStudentAssignment(string UserId);

        Task<ViewStudentAssignent> ViewAssignmentByAssId(string AssId,string AdmissionId);

        Task<bool> StudentUploadHW(StudentUploadAssignment assigment);

        Task<List<ViewFileNameList>> GetUploadedFileName(string AssId, string UserId);
        #endregion
    }
}
