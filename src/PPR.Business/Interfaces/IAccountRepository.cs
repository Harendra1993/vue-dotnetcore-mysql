using System.Collections.Generic;
using PPR.Common.Entities;
namespace PPR.Business.Interfaces
{
    public interface IAccountRepository
    {
        void CreateUser(User userObj);
        IEnumerable<User> GetAllUsers();
        User GetUserByName(string userName);
        User GetUser(string userName, string password);
        User GetUser(int userId);
        bool RequiresUniqueEmail { get; }
        void AddUserRole(UserRole userRole);
        bool IsUserInRole(string username, string roleName);
        string[] GetRolesForUser(string username);
        string[] GetAllRoles();
        //MembershipUser GetUser (string username, bool userIsOnline);

    }
}