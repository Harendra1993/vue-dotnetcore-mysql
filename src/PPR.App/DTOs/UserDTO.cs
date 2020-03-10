using System;
using System.Collections.Generic;
using PPR.Common.Entities;

namespace PPR.App.DTOs {
    public class UserDTO {

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime Created { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
