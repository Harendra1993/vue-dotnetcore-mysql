using System.Collections.Generic;
using PPR.App.DTOs.User;

namespace PPR.App.DTOs
{
    public class RoleDTO
    {

        public short RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

        public virtual ICollection<UserDTO> Users { get; set; }
    }
}
