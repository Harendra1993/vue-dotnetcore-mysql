using AutoMapper;
using VueDotNetMySql.App.DTOs;
using VueDotNetMySql.App.DTOs.User;
using VueDotNetMySql.Common.Entities;

namespace VueDotNetMySql.App.MappingProfiles
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