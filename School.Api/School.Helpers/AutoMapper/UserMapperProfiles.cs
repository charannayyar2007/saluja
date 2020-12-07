using AutoMapper;
using School.Domain.Users;
using School.Dto.Users;

namespace School.Helpers.AutoMapper
{
   public class UserMapperProfiles : Profile
    {
       public UserMapperProfiles()
        {
            CreateMap<UserRegistrationDto, UserRegistertion>();
            CreateMap<UserRegistertion, UserLoginResponseForDto>();
            
        }
           

    }
}
