using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PPR.Business.Interfaces;
using PPR.Common.Entities;
using PPR.Data;

namespace PPR.Business.Repositories {
    public class AccountRepository : IAccountRepository {
        private readonly AppDbContext _dataContext;
        public AccountRepository () {
            _dataContext = new AppDbContext ();
        }

        public bool RequiresUniqueEmail {
            get {
                throw new NotImplementedException ();
            }
        }

        public void AddUserRole (UserRole userRole) {
            var roleEntry = _dataContext.UserRoles.SingleOrDefault (r => r.UserId == userRole.UserId);
            if (roleEntry != null) {
                _dataContext.UserRoles.Remove (roleEntry);
                _dataContext.SaveChanges ();
            }
            _dataContext.UserRoles.Add (userRole);
            _dataContext.SaveChanges ();
        }

        public IEnumerable<User> GetAllUsers () {
            return _dataContext.Users.ToList ();
        }

        public User GetUserByName (string userName) {
            return _dataContext.Users.SingleOrDefault (x => x.UserName == userName);
        }

        public User GetUser (string userName, string password) {
            var user = _dataContext.Users.Include (x => x.UserRoles).ThenInclude (x => x.Role).SingleOrDefault (x => x.UserName == userName && x.Password == password);
            return user;
        }

        public void CreateUser (User userObj) {
            _dataContext.Users.Add (userObj);
            _dataContext.SaveChanges ();
        }

        public bool IsUserInRole (string username, string roleName) {
            var user = _dataContext.Users.SingleOrDefault (u => u.UserName == username);
            if (user == null)
                return false;
            return user.UserRoles != null && user.UserRoles.Select (u => u.Role).Any (r => r.RoleName == roleName);

        }

        public string[] GetRolesForUser (string username) {

            var user = _dataContext.Users.SingleOrDefault (u => u.UserName == username);
            if (user == null)
                return new string[] { };
            return user.UserRoles == null ? new string[] { } : user.UserRoles.Select (u => u.Role).Select (u => u.RoleName).ToArray ();
        }

        public string[] GetAllRoles () {

            return _dataContext.Roles.Select (r => r.RoleName).ToArray ();

        }

    }
}