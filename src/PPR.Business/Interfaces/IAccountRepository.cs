using System.Collections.Generic;
using PPR.Data.Entities;
namespace PPR.Business.Interfaces {
  public interface IAccountRepository {
    void RegisterUser (User userObj);
    IEnumerable<User> GetAllUsers ();
    User GetUserByName (string userName);
    User GetUser (string userName, string password);
    bool RequiresUniqueEmail { get; }
    void AddUserRole (UserRole userRole);
    bool IsUserInRole (string username, string roleName);
    string[] GetRolesForUser (string username);
    string[] GetAllRoles ();
    //MembershipUser GetUser (string username, bool userIsOnline);

  }
}