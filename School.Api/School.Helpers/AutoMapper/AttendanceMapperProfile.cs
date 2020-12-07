using AutoMapper;
using School.Domain.Attendance;
using School.Dto.Attendance;
using System;

namespace School.Helpers.AutoMapper
{
  public  class AttendanceMapperProfile : Profile
    {
        public AttendanceMapperProfile()
        {
            CreateMap<MarkAttendace, MarkAttendanceResposeForDto>();
          
        }
    }
}
