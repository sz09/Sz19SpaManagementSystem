using Service.Business.IServices;
using Service.Business.Services;
using System;
using System.Web.Security;

namespace SMGS.Presentation
{
    public class SPMSRole: RoleProvider
    {
        #region Attributes
        private readonly IAccountServices _iAccountServices;
        private string _applicationName;
        #endregion

        #region Constructors
        public SPMSRole(IAccountServices iAccountServices)
        {
                this._iAccountServices = iAccountServices;
        }
        #endregion

        #region Operations
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            this._iAccountServices.AddUsersToRoles(usernames, roleNames);
        }

        public override string ApplicationName
        {
            get
            {
                return this._applicationName;
            }
            set
            {
                this._applicationName = "SMGS";
            }
        }

        public override void CreateRole(string roleName)
        {
            this._iAccountServices.CreateRole(roleName);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return this._iAccountServices.FindUsersInRole(roleName, usernameToMatch);
        }

        public override string[] GetAllRoles()
        {
            return this._iAccountServices.GetAllRoles();
        }

        public override string[] GetRolesForUser(string username)
        {
            return this._iAccountServices.GetRoleForAccount(username);
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return this._iAccountServices.GetAccountForRole(roleName);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return this._iAccountServices.CheckUserHasRole(username, roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            this.RemoveUsersFromRoles(usernames, roleNames);
        }

        public override bool RoleExists(string roleName)
        {
            return this._iAccountServices.RoleExists(roleName);
        }
        #endregion
    }
}