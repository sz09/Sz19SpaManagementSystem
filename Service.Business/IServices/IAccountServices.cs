using SPMS.ObjectModel.Entities;

namespace Service.Business.IServices
{
    /// <summary>
    /// IAccountServices defines class to implement in class inherited
    ///     Using interface to resolve issue about Account
    ///         This service passes params into operations in IAccountRepository and retrive data
    /// </summary>
    public interface IAccountServices
    {
        /// <summary>
        /// Check user sign in on sign in page
        /// </summary>
        /// <param name="usernameOrCode">
        /// Username of all account
        ///     Code just for Employee
        /// </param>
        /// <param name="password">Password of account fit username</param>
        /// <returns>
        /// true if found account match in database 
        /// otherwise, return false
        /// </returns>
        bool CheckSignIn(string usernameOrCode, string password);
        /// <summary>
        /// Get type of account by Id of type
        /// </summary>
        /// <param name="type">Id of type</param>
        /// <returns>Name of type</returns>
        string AccountFor(int type);
        /// <summary>
        /// Get Account by provided username or code
        /// </summary>
        /// <param name="Username">Username or code of emp</param>
        /// <returns>
        /// true if found
        /// otherwise, return false
        /// </returns>
        Account GetAccountByUsername(string Username);       
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
        System.Tuple<bool, string[]> AutoLoginGetRole(string userName, string xs);
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
        /// <returns>
        /// All roles if found
        ///     Otherwise, return null
        /// </returns>
        string[] GetAllRoles();
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
        /// Get highest role for username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        string GetHighestRole(string userName);
    }
}
