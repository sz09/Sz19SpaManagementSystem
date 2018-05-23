using SPMS.ObjectModel.Entities;
using System;

namespace Core.ObjectServices.Repositories
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Check username or code and password compare and exsting in database
        /// </summary>
        /// <param name="usernameOrCode">
        /// Username for all
        ///     Code for employee
        /// </param>
        /// <param name="password">Password of account</param>
        /// <returns>
        /// True if match
        ///     Otherwise, return false
        /// </returns>
        bool CheckSignIn(string usernameOrCode, string password);

        /// <summary>
        /// Type of Account
        /// </summary>
        /// <param name="type">Type of account</param>
        /// <returns>Name of type</returns>
        string AccountFor(int type);

        /// <summary>
        /// Get Account
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        Account GetAccountByUsername(string Username);
        
        /// <summary>
        /// Create account
        /// </summary>
        /// <param name="account">Object type Account</param>
        bool CreateAccount(Account account);
        /// <summary>
        /// Delete account with param is Account id
        ///     Set IsInUse attribute is false
        /// </summary>
        /// <param name="id">Id of Account</param>
        /// <returns>
        /// If setting value success, return true
        ///     Otherwise, return false
        /// </returns>
        bool DeleteAccount(long id);
        /// <summary>
        /// Delete account with param is Account username
        ///     Set IsInUse attribute is false
        /// </summary>
        /// <param name="userName">Username or emp code</param>
        /// <returns>
        /// If setting value success, return true
        ///     Otherwise, return false
        /// </returns>
        bool DeleteAccount(string userName);
        /// <summary>
        /// Create Xs for auto login feature
        /// </summary>
        /// <param name="userName">username input</param>
        /// <returns>
        /// string Xs if create and save to database success
        ///     Otherwise, return emty string
        ///     </returns>
        string CreateXs(string userName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="xs"></param>
        /// <returns></returns>
        Tuple<bool, string[]> AutoLoginGetRole(string userName, string xs);
        /// <summary>
        /// Get all roles for account
        /// </summary>
        /// <param name="username">Username or code(for employee)</param>
        /// <returns>
        /// Array of string name for role if found
        ///     Otherwise, return null
        /// </returns>
        string[] GetRoleForAccount(string username);
        /// <summary>
        /// Get all accounts for role pass
        /// </summary>
        /// <param name="role">String role pass</param>
        /// <returns>
        /// Array of string account for username or code if found
        ///     Otherwise, return null
        /// </returns>
        string[] GetAccountForRole(string role);
        /// <summary>
        /// Check account has role
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="role">Role</param>
        /// <returns>
        /// If account has role, return true
        ///     Otherwise, return false
        /// </returns>
        bool CheckUserHasRole(string username, string role);
        /// <summary>
        /// Get all roles in databasesde
        /// </summary>
        /// <returns></returns>
        string[] GetAllRoles();
        /// <summary>
        /// Get array of username in role match key
        /// </summary>
        /// <param name="roleName">Role name</param>
        /// <param name="key">Key to match</param>
        /// <returns>
        /// Array of username, if found
        ///     Otherwise, return false
        /// </returns>
        string[] FindUsersInRole(string roleName, string key);
        /// <summary>
        /// Create a role
        /// </summary>
        /// <param name="role">Name of role</param>
        void CreateRole(string role);
        /// <summary>
        /// Add users to roles
        ///     Each user has all roles
        /// </summary>
        /// <param name="usernames"></param>
        /// <param name="roles"></param>
        void AddUsersToRoles(string[] usernames, string[] roles);
        /// <summary>
        /// Remove roles of users
        ///     Each user delete all roles
        /// </summary>
        /// <param name="usernames"></param>
        /// <param name="roles"></param>
        void RemoveUsersFromRoles(string[] usernames, string[] roles);
        /// <summary>
        /// Check role name exists
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        bool RoleExists(string roleName);
        /// <summary>
        /// Delete role from database and save all changes
        /// </summary>
        /// <param name="roleName">Name of role</param>
        /// <param name="throwOnPopulatedRole"></param>
        /// <returns></returns>
        bool DeleteRole(string roleName, bool throwOnPopulatedRole);

        /// <summary>
        /// Get highest role of an account by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        string GetHighestRole(string userName);
    }
}
