using AutoMapper;
using PPR.App.DTOs;
using PPR.Common.Entities;

namespace PPR.App.MappingProfiles {
    public class AccountMappings : Profile {
        public AccountMappings () {
            CreateMap<User, UserDTO> ().ReverseMap ();
        }
    }
}