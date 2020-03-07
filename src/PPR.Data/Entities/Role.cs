using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PPR.Data.Entities {
    public class Role {

        public short RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}