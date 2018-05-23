using Core.ObjectServices.Repositories;
using System;
using Service.Business.IServices;
using SPMS.ObjectModel.Entities;
using log4net;
using Infrastructure.Logging;

namespace Service.Business.Services
{
    public class AccountServices : IAccountServices
    {
        #region Attributes
        private readonly IAccountRepository _iAccountRepositories;
        private static readonly ILog logger = LogManager.GetLogger(typeof(AccountServices));
        #endregion

        #region Constructors
        public AccountServices(IAccountRepository iAccountRepositories)
        {
            logger.EnterMethod();
            this._iAccountRepositories = iAccountRepositories;
            logger.Info("Success setting value to attributes");
            logger.LeaveMethod();
        }
        #endregion

        #region Functions and Operations
        public bool CheckSignIn(string usernameOrCode, string password)
        {
            logger.EnterMethod();
            try
            {
                return this._iAccountRepositories.CheckSignIn(usernameOrCode, password);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return false;
            }
            finally
            {
                logger.LeaveMethod();
            }
            
        }
        public string AccountFor(int type)
        {
            logger.EnterMethod();
            try
            {
                return this._iAccountRepositories.AccountFor(type);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return "";
            }
            finally
            {
                logger.LeaveMethod();
            }
        }
        public Account GetAccountByUsername(string Username)
        {
            logger.EnterMethod();
            try
            {
                return this._iAccountRepositories.GetAccountByUsername(Username);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public string CreateXs(string userName)
        {
            logger.EnterMethod();
            try
            {
                return this._iAccountRepositories.CreateXs(userName);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public Tuple<bool, string[]> AutoLoginGetRole(string userName, string xs)
        {
            logger.EnterMethod();
            try
            {
                return this._iAccountRepositories.AutoLoginGetRole(userName, xs);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }
        public string[] GetRoleForAccount(string username)
        {
            logger.EnterMethod();
            try
            {
                return this._iAccountRepositories.GetRoleForAccount(username);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public string[] GetAccountForRole(string role)
        {
            logger.EnterMethod();
            try
            {
                return this._iAccountRepositories.GetAccountForRole(role);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }
        public bool CheckUserHasRole(string username, string role)
        {
            logger.EnterMethod();
            try
            {
                return this._iAccountRepositories.CheckUserHasRole(username, role);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return false;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public string[] GetAllRoles()
        {
            logger.EnterMethod();
            try
            {
                return this._iAccountRepositories.GetAllRoles();
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public string[] FindUsersInRole(string roleName, string key)
        {
            logger.EnterMethod();
            try
            {
                return this._iAccountRepositories.FindUsersInRole(roleName, key);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public void CreateRole(string role)
        {
            logger.EnterMethod();
            try
            {
                this._iAccountRepositories.CreateRole(role);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public void AddUsersToRoles(string[] usernames, string[] roles)
        {
            logger.EnterMethod();
            try
            {
                this._iAccountRepositories.AddUsersToRoles(usernames, roles);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public void RemoveUsersFromRoles(string[] usernames, string[] roles)
        {
            logger.EnterMethod();
            try
            {
                this._iAccountRepositories.RemoveUsersFromRoles(usernames, roles);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public bool RoleExists(string roleName)
        {
            logger.EnterMethod();
            try
            {
                return this._iAccountRepositories.RoleExists(roleName);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return false;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public string GetHighestRole(string userName)
        {
            logger.EnterMethod();
            try
            {
                return this._iAccountRepositories.GetHighestRole(userName);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }
        #endregion
    }
}
