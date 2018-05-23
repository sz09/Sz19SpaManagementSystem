using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Core.ObjectServices.Repositories;
using SPMS.ObjectModel.Entities;
using log4net;
using Infrastructure.Logging;
using Core.ObjectServices.UnitOfWork;
using System.Transactions;
using System.Collections.Generic;
using SMGS.Presentation.Classes;

namespace Infrastructure.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        #region Attributes
        private readonly IRepository<Account> _accountRepositories;
        private readonly IRepository<AccountFor> _accountForRepositories;
        private readonly IRepository<AccountAutoLogin> _accountAutoLoginRepositories;
        private readonly IRepository<AccountMappingRole> _accountMappingRoleRepositories;
        private readonly IUnitOfWork _iUnitOfWork;
        private static readonly ILog logger = LogManager.GetLogger(typeof(AccountRepository));
        private static readonly ushort xsLength = 100;
        #endregion

        #region Constructors
        public AccountRepository(IRepository<Account> accountRepositories, IRepository<AccountFor> accountForRepositories, IRepository<AccountAutoLogin> accountAutoLoginRepositories, IRepository<AccountMappingRole> accountMappingRoleRepositories, IUnitOfWork iUnitOfWork)
        {
            logger.EnterMethod();
            try 
	        {	        
                this._accountRepositories = accountRepositories;
                this._accountForRepositories = accountForRepositories;
                this._accountAutoLoginRepositories = accountAutoLoginRepositories;
                this._accountMappingRoleRepositories = accountMappingRoleRepositories;
                this._iUnitOfWork = iUnitOfWork;
                logger.Info("Success setting values for attributes");
	        }
	        catch (Exception e)
	        {
                logger.Error("Error: " + e.Message);
	        }
	        finally
	        {
                logger.LeaveMethod();
	        }
        }
        #endregion

        #region Operations
        public bool CheckSignIn(string usernameOrCode, string password)
        {
            logger.EnterMethod();
            try
            {
                string md5Pass = ConvertToMD5(password);
                var isFoundAccount = this._accountRepositories.Find(_ => _.UserName == usernameOrCode && _.Password == md5Pass && _.IsInUse == true).FirstOrDefault();
                if (isFoundAccount != null)
                {
                    logger.Info("Found account match username and password with username: [" + usernameOrCode + "]");
                    return true;
                }
                else
                {
                    isFoundAccount = this._accountRepositories.Find(_ => _.ForCode == usernameOrCode && _.Password == md5Pass).FirstOrDefault();
                    if (isFoundAccount != null)
                    {
                        logger.Info("Found account match username and password with code: [" + usernameOrCode + "]");
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
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
                var accountGet = this._accountForRepositories.Get(_ => _.Id == type);
                if (accountGet != null)
                {
                    logger.Info("Found type of account: [" + accountGet.ForType + "]");
                    return accountGet.ForType;
                }
                return "";
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
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
                var accountGet = this._accountRepositories.Get(_ => _.UserName == Username || _.ForCode == Username);
                if (accountGet != null)
                {
                    logger.Info("Found account, username: [" + accountGet.UserName + "]");
                    return accountGet;
                }
                else
                {
                    logger.Info("Can't find account with param: [" + Username + "] ");
                    return null;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error: " + e.Message);
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }
        public bool CreateAccount(Account account)
        {
            logger.EnterMethod();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    this._accountRepositories.Add(account);
                        
                    transactionScope.Complete();
                }
                this._iUnitOfWork.Save();
                logger.Info("Create account success username: [" + account.UserName + "]");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("Error: [" + ex.Message + "]");
                return false;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }
        public bool DeleteAccount(long id)
        {
            logger.EnterMethod();
            try
            {
                var account = this._accountRepositories.Get(_ => _.Id == id);
                if(account != null)
                {
                    account.IsInUse = false;
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._accountRepositories.Update(account);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Deleted account with Id: [" + id + "]");
                    return true;
                }
                else
                {
                    logger.Info("Can't find any account with Id: [" + id + "]");
                    return false;
                }
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
        public bool DeleteAccount(string userName)
        {
            logger.EnterMethod();
            try
            {
                var account = GetAccountByUsername(userName);
                if(account != null)
                {
                    account.IsInUse = false;
                    using(TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._accountRepositories.Update(account);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Deleted account with Id: [" + account.Id + "]");
                    return true;
                }
                else
                {
                    logger.Info("Can't find any account fit username inputed: [" + userName + "]");
                    return false;
                }
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
        public string CreateXs(string userName)
        {
            logger.EnterMethod();
            try
            {
                string xs = "";
                var username = this._accountRepositories.Get(_=>_.UserName == userName);
                if (username != null)
                {
                    for (int i = 0; i < xsLength; i++)
                    {
                        xs += CreateRandomChar();
                    }
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._accountAutoLoginRepositories.Add(new AccountAutoLogin()
                        {
                            Username = userName,
                            Xs = xs
                        });
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                }
                return xs;
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
                bool checkSignIn = false;
                string[] role;
                var acc = this._accountAutoLoginRepositories.Get(_ => _.Username == userName && _.Xs == xs);
                if (acc != null)
                {
                    checkSignIn = true;
                    var accRole = this._accountRepositories.Get(_ => _.UserName == userName || _.ForCode == userName);
                    if (accRole != null)
                    {
                        var allRolesForAcc = this._accountMappingRoleRepositories.Find(_ => _.AccountId == accRole.Id).ToList();
                        if (allRolesForAcc != null)
                        {
                            role = new string[allRolesForAcc.Count];
                            for (int i = 0; i < allRolesForAcc.Count; i++)
                            {
                                role[i] = allRolesForAcc[i].AccountFor.ForType;
                            }
                            return Tuple.Create(checkSignIn, role);
                        }
                        else
                        {
                            return Tuple.Create(checkSignIn, new string[0]);
                        }
                    }
                    else
                    {
                        return Tuple.Create(checkSignIn, new string[0]);
                    }
                }
                else
                {
                    return Tuple.Create(false, new string[0]);
                }
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return Tuple.Create(false, new string[0]);
            }
            finally
            {
                logger.LeaveMethod();
            }
        }
        private char CreateRandomChar()
        {
            Random rdm = new Random();
            int random = rdm.Next(1, 3);
            switch (random)
            {
                case 1:
                    random = rdm.Next(48, 57);
                    break;
                case 2:
                    random = rdm.Next(65, 90);
                    break;
                case 3:
                    random = rdm.Next(97, 122);
                    break;
            }
            return (char)random;
        }

        public string[] GetRoleForAccount(string username)
        {
            logger.EnterMethod();
            try
            {
                var userNameGet = this._accountRepositories.Get(_ => _.UserName == username || _.ForCode == username);
                if (userNameGet != null)
                {
                    var allrole = this._accountMappingRoleRepositories.Find(_ => _.AccountId == userNameGet.Id).ToList();
                    if (allrole != null)
                    {
                        string[] roleArray = new string[allrole.Count];
                        for (int i = 0; i < allrole.Count; i++)
                        {
                            roleArray[i] = allrole[i].AccountFor.ForType;
                        }
                        logger.Info("Found role for username [" + username + "]");
                        return roleArray;
                    }
                    else return new string[0];
                }
                else return new string[0];
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return new string[0];
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
                var roleGet = this._accountForRepositories.Get(_ => _.ForType == role);
                if (roleGet != null)
                {
                    var mapping = this._accountMappingRoleRepositories.Find(_ => _.RoleId == roleGet.Id).ToList();
                    if (mapping != null)
                    {
                        string[] arrAcc = new string[mapping.Count];
                        for (int i = 0; i < mapping.Count; i++)
                        {
                            arrAcc[i] = mapping[i].Account.UserName;
                        }
                        logger.Info("Found [" + mapping.Count + "] account in role [" + role + "]");
                        return arrAcc;
                    }
                    else return new string[0];
                }
                else
                {
                    logger.Info("Can't found role with name: [" + role + "]");
                    return null;
                }
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
                bool check = false;
                var account = this._accountRepositories.Get(_ => (_.UserName == username ||
                                                    _.ForCode == username));
                var roleGet = this._accountForRepositories.Get(_ => _.ForType == role);

                if (account != null && roleGet != null)
                {
                    if (this._accountMappingRoleRepositories.Get(_ => _.RoleId == roleGet.Id &&
                                                                   _.AccountId == account.Id) != null)
                    {
                        check = true;
                        logger.Info("Username: [" + username + "] has role: [" + role + "]");
                    }
                    else
                    {
                        logger.Info("Username: [" + username + "] hasn't role: [" + role + "]");
                    }
                }
                else
                {
                    logger.Info("Username or role pass not found");
                }
                return check;
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
                var allRoles = this._accountForRepositories.GetAll().ToList();
                string[] arrRoles;
                if (allRoles != null)
                {
                    arrRoles = new string[allRoles.Count];
                    for (int i = 0; i < allRoles.Count; i++)
                    {
                        arrRoles[i] = allRoles[i].ForType;
                    }
                    logger.Info("Found [" + allRoles.Count + "] roles");
                    return arrRoles;
                }
                return null;
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
                string[] usernames;
                var role = this._accountMappingRoleRepositories.Find(_=> (_.AccountFor.ForType == roleName &&
                                                                          _.Account.UserName.Contains(key))).ToList();
                if (role != null)
                {
                    usernames = new string[role.Count];
                    for (int i = 0; i < role.Count; i++)
                    {
                        usernames[i] = role[i].Account.UserName;
                    }
                    logger.Info("Found [" + role.Count + "] username fit role [" + roleName + "] and key [" + key + "]");
                    return usernames;
                }
                else
                {
                    logger.Info("Can't found any username fit role [" + roleName + "] and key [" + key + "]");
                    return new string[0];
                }
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
                var roleGet = this._accountForRepositories.Get(_ => _.ForType == role);
                if (roleGet == null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        var accountFor = new SPMS.ObjectModel.Entities.AccountFor() { ForType = role };
                        this._accountForRepositories.Add(accountFor);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Create role with name [" + role + "]");
                }
                else
                    logger.Info("Role name: [" + role + "] exists. Don't create role");
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
                List<Account> listAccountFitUsername = new List<Account>();
                for(int i = 0; i < usernames.Length; i ++)
                {
                    var acc = this._accountRepositories.Get(_=>_.UserName == usernames[i] ||
                                                               _.ForCode == usernames[i]);
                    listAccountFitUsername.Add(acc ?? new Account() { Id = -1 });
                }
                List<AccountFor> listAccountForFitRoles = new List<AccountFor>();
                for (int i = 0; i < roles.Length; i++)
                {
                    var role = this._accountForRepositories.Get(_=>_.ForType == roles[i]);
                    listAccountForFitRoles.Add(role ?? new AccountFor() { Id = -1 });
                }
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    for (int i = 0; i < listAccountFitUsername.Count; i++)
                    {
                        if(listAccountFitUsername[i].Id != -1)
                            for (int j = 0; j < listAccountForFitRoles.Count; j++)
                            {
                                if (listAccountForFitRoles[j].Id != -1)
                                {
                                    this._accountMappingRoleRepositories.Add(new AccountMappingRole()
                                    {
                                        AccountId = listAccountFitUsername[i].Id,
                                        RoleId = listAccountForFitRoles[j].Id
                                    });
                                }
                            }
                    }
                    transactionScope.Complete();
                }
                logger.Info("Add users to roles success");
                this._iUnitOfWork.Save();
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
                List<Account> listAccountFitUsername = new List<Account>();
                for (int i = 0; i < usernames.Length; i++)
                {
                    var acc = this._accountRepositories.Get(_ => _.UserName == usernames[i] ||
                                                               _.ForCode == usernames[i]);
                    listAccountFitUsername.Add(acc ?? new Account() { Id = -1 });
                }
                List<AccountFor> listAccountForFitRoles = new List<AccountFor>();
                for (int i = 0; i < roles.Length; i++)
                {
                    var role = this._accountForRepositories.Get(_ => _.ForType == roles[i]);
                    listAccountForFitRoles.Add(role ?? new AccountFor() { Id = -1 });
                }
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    for (int i = 0; i < listAccountFitUsername.Count; i++)
                    {
                        if (listAccountFitUsername[i].Id != -1)
                        for (int j = 0; j < listAccountForFitRoles.Count; j++)
                        {
                            if(listAccountForFitRoles[j].Id != -1)
                            {
                                var mappingGet = this._accountMappingRoleRepositories.Get(_ => (_.AccountId == listAccountFitUsername[i].Id &&
                                                                                                _.RoleId == listAccountForFitRoles[i].Id));
                                if (mappingGet != null)
                                {
                                    this._accountMappingRoleRepositories.Delete(mappingGet);
                                }
                            }
                        }
                    }
                    transactionScope.Complete();
                }
                logger.Info("Remove users to roles success");
                this._iUnitOfWork.Save();
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
                var role = this._accountForRepositories.Get(_ => _.ForType == roleName);
                if (role != null)
                {
                    return true;
                }
                return false;
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

        #region MD5
        /// <summary>
        /// Call GetMd5Hash function
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string ConvertToMD5(string text)
        {
            using (MD5 md5Hasher = MD5.Create()){
                return GetMd5Hash(md5Hasher, text);
            }
        }
        /// <summary>
        /// Encoding string to encoded string
        /// </summary>
        /// <param name="md5Hash">MD5 hasher</param>
        /// <param name="input">string</param>
        /// <returns>encoded string</returns>
        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        #endregion


        public bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public string GetHighestRole(string userName)
        {
            logger.EnterMethod();
            try
            {
                var roleReturn = "";
                var roles = (from role in this._accountMappingRoleRepositories.Find(_ => (_.Account.UserName == userName)).ToList()
                             select role.AccountFor).ToList();
                if (!roles.Any())
                {
                    
                    logger.Info("Can't found any role for user with account: [" + userName + "]");
                    return null;
                }
                else
                {
                    if (roles.Any() && roles.Count == 1)
                    {
                        roleReturn = roles[0].ForType;
                    }
                    else
                    {
                        roleReturn = HighestRole(roles).ForType;
                    }
                }
                logger.Info(userName + " has role: [" + roleReturn + "] as highest role");
                return roleReturn;
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

        private AccountFor HighestRole(List<AccountFor> accountFors)
        {
            int min = int.MaxValue;
            foreach (var accountFor in accountFors)
            {
                foreach (Enums.Account item in Enum.GetValues(typeof(Enums.Account)))
                {
                    if ((int)item < min && accountFor.ForType == item.ToString())
                    {
                        min = accountFor.Id;
                    }
                }
            }

            return accountFors.FirstOrDefault(_ => _.Id == min);
        }
        #endregion
    }
}
