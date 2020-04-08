using AutoMapper;
using VueDotNetCoreMySql.App.DTOs;
using VueDotNetCoreMySql.App.DTOs.User;
using VueDotNetCoreMySql.Common.Entities;

namespace VueDotNetCoreMySql.App.MappingProfiles
{
    public class AccountMappings : Profile
    {
        public AccountMappings()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserCreateDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
            CreateMap<UserRole, UserRoleDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
        }
    }
}