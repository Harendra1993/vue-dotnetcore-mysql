using System;
using System.Collections.Generic;

namespace PPR.App.DTOs {
    public class UserDTO {

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime Created { get; set; }
        public string Token { get; set; }

        public string[] UserRoles { get; set; }
    }
}