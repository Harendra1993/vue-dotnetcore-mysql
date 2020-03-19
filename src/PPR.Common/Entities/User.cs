using System;
using System.Collections.Generic;

namespace PPR.Common.Entities {
    public class User {

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime Created { get; set; }
        public Boolean IsActive { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}