using System.ComponentModel.DataAnnotations;

namespace VueDotNetCoreMySql.App.DTOs
{
    public class UserRoleDTO
    {
        [Key]
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public short RoleId { get; set; }

        public virtual RoleDTO Role { get; set; }
    }
}
