using School.Domain.HomeWork;
using School.Dto.HomeWorks;
using School.Dto.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Contract
{
    public interface IHomeWorkService
    {
        #region Teacher
        Task<bool> UploadAssignment(TeacherAssignmentForDto assignmentFrDto);
        
            Task<bool> EditAssignment(TeacherAssignmentForDto assignmentFrDto);
        Task<List<TeacherViewAssignmentForDto>> ViewAllAssignment(string UserId);

        Task<List<TeacherUploadAssignmentForDto>> ViewAssignmentByTeacher(string UserId);
        // Task<List<ViewAssignmentForDto>> ViewAssignmentbyStudent(string UserId);

        Task<List<string>> DeleteHW(int FileId);
        Task<List<TeacherDownloadAssignmentForDto>> DownloadHW(int FileId);
        Task<TeacherAssignmentForDto> EditHW(int FileId);
        Task<List<TeacherUploadAssignmentForDto>> SearchHWByTeacher(int ClassId, int SubId, string date, string UserId);

        Task<List<TeacherViewAssignmentForDto>> SearchViewHW(int ClassId, int SubId, string date,string UserId);
        #endregion

        #region Student
        Task<List<ViewStudentAssignmentFrDto>> ViewStudentAssignment(string UserId);

        Task<ViewStudentAssignmentFrDto> ViewAssignmentByAssId(string AssId,string AdmissionId);

        Task<bool> StudentUploadHW(StudentAssignmentForDto assignmentForDto);
        Task<List<ViewFileNameListForDto>> GetUploadedFileName(string AddId, string UserId);

      

        #endregion
    }
}
