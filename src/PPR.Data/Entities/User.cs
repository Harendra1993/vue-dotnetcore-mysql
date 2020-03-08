using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PPR.Data.Entities {
    public class User {

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}