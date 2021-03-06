﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using VueDotNetCoreMySql.Business.Interfaces;
using VueDotNetCoreMySql.Common.Entities;
using VueDotNetCoreMySql.Data;

namespace VueDotNetCoreMySql.Business.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _dataContext;
        public AccountRepository()
        {
            _dataContext = new AppDbContext();
        }

        public bool RequiresUniqueEmail
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void AddUserRole(UserRole userRole)
        {
            var roleEntry = _dataContext.UserRoles.SingleOrDefault(r => r.UserId == userRole.UserId);
            if (roleEntry != null)
            {
                _dataContext.UserRoles.Remove(roleEntry);
                _dataContext.SaveChanges();
            }
            _dataContext.UserRoles.Add(userRole);
            _dataContext.SaveChanges();
        }
        public void AddUserRoles(IEnumerable<UserRole> userRoles)
        {
            _dataContext.UserRoles.AddRange(userRoles);
            _dataContext.SaveChanges();
        }
        public IEnumerable<User> GetAllUsers(int currentUserId)
        {
            return _dataContext.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).Where(x => x.UserId != currentUserId).ToList();
        }

        public User GetUserByName(string userName)
        {
            return _dataContext.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public User GetUser(string userName, string password)
        {
            var user = _dataContext.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).SingleOrDefault(x => x.UserName == userName && x.Password == password && x.IsActive);
            return user;
        }
        public User GetUser(int userId)
        {
            var user = _dataContext.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).SingleOrDefault(x => x.UserId == userId);
            return user;
        }

        public void CreateUser(User userObj)
        {
            _dataContext.Users.Add(userObj);
            _dataContext.SaveChanges();
        }

        public bool IsUserInRole(string username, string roleName)
        {
            var user = _dataContext.Users.SingleOrDefault(u => u.UserName == username);
            if (user == null)
                return false;
            return user.UserRoles != null && user.UserRoles.Select(u => u.Role).Any(r => r.RoleName == roleName);

        }

        public string[] GetRolesForUser(string username)
        {

            var user = _dataContext.Users.SingleOrDefault(u => u.UserName == username);
            if (user == null)
                return new string[] { };
            return user.UserRoles == null ? new string[] { } : user.UserRoles.Select(u => u.Role).Select(u => u.RoleName).ToArray();
        }

        public IEnumerable<Role> GetAllRoles()
        {

            return _dataContext.Roles.ToList<Role>();

        }

        public void UpdateUserLastLogin(int userId)
        {
            var userObj = _dataContext.Users.First(x => x.UserId == userId);
            userObj.LastLogin = DateTime.UtcNow;
            _dataContext.UpdateRange(userObj);
            _dataContext.SaveChanges();
        }

        public User UpdateUser(User userObj)
        {
            var obj = _dataContext.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).First(x => x.UserId == userObj.UserId);
            obj.IsActive = userObj.IsActive;
            obj.UserRoles = userObj.UserRoles;
            _dataContext.UpdateRange(obj);
            _dataContext.SaveChanges();
            return obj;
        }

        public void RemoveUser(int userId)
        {
            var user = _dataContext.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).First(x => x.UserId == userId);
            if (user != null)
            {
                _dataContext.Users.Remove(user);
                _dataContext.SaveChanges();
            }
        }
    }
}