using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Application.Contract;
using Infra.School.Data.Contract;
using Infra.School.Data.DataFactory;
using Infra.School.Data.HomeWorksRepository;
using School.Domain.HomeWork;
using School.Dto.HomeWorks;
using School.Dto.Master;

namespace School.Application.HomeWorksService
{
    public class HomeWorkService : BaseService,IHomeWorkService 
    {
        private readonly IHomeWorkRepository homeWorkRepository;
        public HomeWorkService()
        {
            homeWorkRepository = FactoryDataLayer<HomeworkRepository>.Create();
        }

        #region Teacher
        public async Task<bool> UploadAssignment(TeacherAssignmentForDto assignment)
        {
            var uploadfile = mapper.Map<TeacherAssignment>(assignment);
            return await homeWorkRepository.UploadFile(uploadfile);
        }

        
             public async Task<bool> EditAssignment(TeacherAssignmentForDto assignment)
        {
            var uploadfile = mapper.Map<TeacherAssignment>(assignment);
            return await homeWorkRepository.EditAssignment(uploadfile);
        }
        public async Task<List<TeacherViewAssignmentForDto>> ViewAllAssignment(string UserId)
        {
            var res = await homeWorkRepository.ViewAllAssignment(UserId);
            var result = mapper.Map<List<TeacherViewAssignmentForDto>>(res);
            return result;
        }

        //public async Task<List<ViewAssignmentForDto>> ViewAssignmentbyStudent(string UserId)
        //{
        //    var res = await homeWorkRepository.ViewAssignmentbyStudent(UserId);
        //    var result = mapper.Map<List<ViewAssignmentForDto>>(res);
        //    return result;
        //}

        public async Task<List<TeacherUploadAssignmentForDto>> ViewAssignmentByTeacher(string UserId)
        {
            var res= await homeWorkRepository.ViewAssignmentByTeacher(UserId);
            var result = mapper.Map<List<TeacherUploadAssignmentForDto>>(res);
            return result;
        }

        public async Task<List<string>> DeleteHW(int FileId)
        {
            return await homeWorkRepository.DeleteHW(FileId);
        }

        public async Task<List<TeacherDownloadAssignmentForDto>> DownloadHW(int FileId)
        {
            var res= await homeWorkRepository.DownloadHW(FileId);
            var result = mapper.Map<List<TeacherDownloadAssignmentForDto>>(res);
            return result;
        }
        public async Task<TeacherAssignmentForDto> EditHW(int FileId)
        {
            var re= await homeWorkRepository.EditHW(FileId);
            var res = mapper.Map<TeacherAssignmentForDto>(re);
            return res;
        }

        public async Task<List<TeacherUploadAssignmentForDto>> SearchHWByTeacher(int ClassId, int SubId, string date, string UserId)
        {
          
            var res = await homeWorkRepository.SearchHWByTeacher(ClassId, SubId, date, UserId);
            var result = mapper.Map<List<TeacherUploadAssignmentForDto>>(res);
            return result;
        }

        public async Task<List<TeacherViewAssignmentForDto>> SearchViewHW(int ClassId, int SubId, string date, string UserId)
        {
            var res = await homeWorkRepository.SearchViewHW(ClassId, SubId, date,UserId);
            var result = mapper.Map<List<TeacherViewAssignmentForDto>>(res);
            return result;
        }

        #endregion

        #region Student
        public async Task<List<ViewStudentAssignmentFrDto>> ViewStudentAssignment(string UserId)
        {
            var res = await homeWorkRepository.ViewStudentAssignment(UserId);
            var result = mapper.Map<List<ViewStudentAssignmentFrDto>>(res);
            return result;
        }
        public async Task<ViewStudentAssignmentFrDto> ViewAssignmentByAssId(string AssId,string AdmissionID)
        {
            var res = await homeWorkRepository.ViewAssignmentByAssId(AssId,AdmissionID);
              var result = mapper.Map<ViewStudentAssignmentFrDto>(res);
                return result;
           
        }

        public async Task<bool> StudentUploadHW(StudentAssignmentForDto assignmentForDto)
        {
            var uploadfile = mapper.Map<StudentUploadAssignment>(assignmentForDto);
            return await homeWorkRepository.StudentUploadHW(uploadfile);
        }

        public async Task<List<ViewFileNameListForDto>> GetUploadedFileName(string AssId,string UserId)
        {
            var res = await homeWorkRepository.GetUploadedFileName(AssId, UserId);
            var result = mapper.Map<List<ViewFileNameListForDto>>(res);
            return result;
        }
        #endregion
    }
}
