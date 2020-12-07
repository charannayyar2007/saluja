using AutoMapper;
using School.Domain.HomeWork;
using School.Dto.HomeWorks;

namespace School.Helpers.AutoMapper
{
    public class HomeWorkMapperProfiles : Profile
    {
        public HomeWorkMapperProfiles()
        {
            
            // For Upload Mapping
            CreateMap<TeacherAssignment, TeacherAssignmentForDto>();
            CreateMap<StudentUploadAssignment, StudentAssignmentForDto>();
            CreateMap<TeacherAssignmentForDto, TeacherAssignment>();
            CreateMap<StudentAssignmentForDto, StudentUploadAssignment>();
            // For view Mapping

            CreateMap<TeacherViewAssignment, TeacherViewAssignmentForDto>();
            CreateMap<TeacherAssignmentForDto, TeacherAssignment>();
            CreateMap<TeacherUploadAssignment, TeacherUploadAssignmentForDto>();
            CreateMap<TeacherUploadAssignmentForDto, TeacherUploadAssignment>();

           
            CreateMap<ViewStudentAssignmentFrDto, ViewStudentAssignent>();
            CreateMap<ViewStudentAssignent, ViewStudentAssignmentFrDto>();

            CreateMap<ViewFileNameList, ViewFileNameListForDto>();
            CreateMap<TeacherDownloadAssignment, TeacherDownloadAssignmentForDto>();
           
        }
    }
}
