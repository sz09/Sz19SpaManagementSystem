using System;
using System.Collections.Generic;
using System.Linq;
using Core.ObjectServices.Repositories;
using SPMS.ObjectModel.Entities;
using System.Linq.Expressions;
using Core.ObjectServices.UnitOfWork;
using System.Transactions;
using log4net;
using Infrastructure.Logging;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// CustomerRepository is implemented ICustomerRepository
    /// This class provides all operations to access Customer Entity
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        #region Attributes
        private readonly IRepository<Customer> _customerRepository;
        private readonly IUnitOfWork _iUnitOfWork;
        private static readonly ILog logger = LogManager.GetLogger(typeof(CustomerRepository));
        private const int codeEmpLength = 8; // CustomerCode length is 8
        private readonly int empPerPage;
        private const int defaultEmpPerPage = 20;
        #endregion
        #region Constructors
        public CustomerRepository(IRepository<Customer> _customerRepository, IUnitOfWork iUnitOfWork )
        {
            logger.EnterMethod();
            this._customerRepository = _customerRepository;
            this._iUnitOfWork = iUnitOfWork;
            try
            {
                this.empPerPage = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["empPerPage"]);
                logger.Info("Success setting value to empPerPage attribute value: [" + this.empPerPage.ToString() + "]");
            }
            catch (Exception ex)
            {
                this.empPerPage = defaultEmpPerPage;
                logger.Error("Error:[" + ex.Message + "]. Setting default value: [" + this.empPerPage.ToString() + "]");
            }
            logger.LeaveMethod();
        }
        #endregion

        #region Operations
        /// <summary>
        /// Insert Customer to Repository and save changes
        /// </summary>
        /// <param name="customer">Object customer</param>
        /// <returns>
        /// true: If insert Customer success
        /// Otherwise, false
        /// </returns>
        public bool InsertCustomer(Customer customer)
        {
            logger.EnterMethod();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    this._customerRepository.Add(customer);
                    transactionScope.Complete();
                }
                this._iUnitOfWork.Save();
                logger.Info("Success insert customer with Id: [" + customer.Id + "]  into database and save all changes");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("Error: " + ex.Message);
                return false;
            }
            finally
            {
                logger.LeaveMethod();
            }
            
        }

        /// <summary>
        /// Update customer in Repository and save all changes
        /// </summary>
        /// <param name="customer">Object customer</param>
        /// <returns>
        /// true: If update Customer success
        /// Otherwise, false
        /// </returns>
        public bool UpdateCustomer(Customer customer)
        {
            logger.EnterMethod();
            try
            {
                var customerGot = this._customerRepository.Get(customer.Id);
                if (customerGot != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        customerGot.FirstName = customer.FirstName;
                        customerGot.LastMiddle = customer.LastMiddle;
                        customerGot.Summary = customer.Summary;
                        customerGot.Image = customer.Image;
                        customerGot.CustomerCode = customer.CustomerCode;
                        this._customerRepository.Update(customerGot);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Success update customer with Id: [" + customer.Id + "]in database and save all changes");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.Error("Error: " + ex.Message);
                return false;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            logger.EnterMethod();
            try
            {
                var customerGot = this._customerRepository.Get(customerId);
                if (customerGot != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._customerRepository.Delete(customerGot);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Success detele customer with Id: [" + customerId + "] from database and save all changes");
                    return true;
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

        public bool DeleteCustomer(params object[] keyValues)
        {
            logger.EnterMethod();
            try
            {
                var customerGot = this._customerRepository.Get(keyValues);
                if (customerGot != null) 
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._customerRepository.Delete(customerGot);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();

                    logger.Info("Success detele customer with keys: [" + keyValues + "] from database and save all changes");
                    return true;
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

        public bool Delete(IEnumerable<Customer> listCustomer)
        {
            logger.EnterMethod();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (var item in listCustomer)
                    {
                        var customerGot = this._customerRepository.Get(item);
                        if (customerGot != null)
                        {
                            this._customerRepository.Delete(customerGot);
                        }
                    }
                    transactionScope.Complete();
                }
                this._iUnitOfWork.Save();
                logger.Info("Success delete list customers: [" + listCustomer.Count() + " customer(s)]");
                return true;
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


        public Customer Get(long customerId)
        {
            logger.EnterMethod();
            try
            {
                var emp = this._customerRepository.Get(_ => _.Id == customerId);
                if (emp != null)
                    logger.Info("Got customer with Id: [" + customerId + "]");
                return emp;
            }
            catch (Exception ex)
            {
                logger.Error("Error: " + ex.Message);
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public Customer Get(params object[] keyValues)
        {
            logger.EnterMethod();
            try
            {

            }
            catch (Exception ex)
            {

                logger.Error("Error: " + ex.Message);
            }
            finally
            {
                logger.LeaveMethod();
            }
            return this._customerRepository.Get(keyValues);
        }

        public Customer Get(Expression<Func<Customer, bool>> predicated)
        {
            logger.EnterMethod();
            try
            {

            }
            catch (Exception ex)
            {

                logger.Error("Error: " + ex.Message);
            }
            finally
            {
                logger.LeaveMethod();
            }
            return this._customerRepository.Get(predicated);
        }

        public Customer Get(Expression<Func<Customer, bool>> predicated, params Expression<Func<Customer, object>>[] includes)
        {
            logger.EnterMethod();
            try
            {
                var emp = this._customerRepository.Get(predicated, includes);
                if (emp != null) 
                    logger.Info("Got customer with Id: [" + emp.Id + "]");
                return emp;
            }
            catch (Exception ex)
            {
                logger.Error("Error: " + ex.Message);
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public IQueryable<Customer> GetAll()
        {
            logger.EnterMethod();
            try
            {
                return this._customerRepository.GetAll();
            }
            catch (Exception ex)
            {
                logger.Error("Error: " + ex.Message);
                return null;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public IQueryable<Customer> GetAll(params Expression<Func<Customer, object>>[] includes)
        {
            logger.EnterMethod();
            try
            {

            }
            catch (Exception ex)
            {

                logger.Error("Error: " + ex.Message);
            }
            finally
            {
                logger.LeaveMethod();
            }
            return this._customerRepository.GetAll(includes);
        }

        public IQueryable<Customer> Find(Expression<Func<Customer, bool>> predicated)
        {
            logger.EnterMethod();
            try
            {

            }
            catch (Exception ex)
            {

                logger.Error("Error: " + ex.Message);
            }
            finally
            {
                logger.LeaveMethod();
            }
            return this._customerRepository.Find(predicated);
        }

        public IQueryable<Customer> Find(Expression<Func<Customer, bool>> predicated, params Expression<Func<Customer, object>>[] includes)
        {
            logger.EnterMethod();
            try
            {

            }
            catch (Exception ex)
            {

                logger.Error("Error: " + ex.Message);
            }
            finally
            {
                logger.LeaveMethod();
            }
            return this._customerRepository.Find(predicated, includes);

        }

        /// <summary>
        /// Create new Code for Employee
        ///     Random capitalization char 
        ///         With constant length of characters
        /// </summary>
        /// <returns>Code for Employee with length is constant variabble codeEmpLength</returns>
        public string CreateNewCode()
        {
            string code = "";
            bool coincidentCode = false;
            Random rdm = new Random();
            do{
                for (int i = 0; i < codeEmpLength; i++)
                {
                    code+= (char)rdm.Next(65, 90);
                }
                // Check if coincident
                coincidentCode = CheckCoincidentCode(code);
            } while (coincidentCode);
            return code;
        }

        private bool CheckCoincidentCode(string empCode)
        {
            logger.EnterMethod();
            try
            {

            }
            catch (Exception ex)
            {
                logger.Error("Error: " + ex.Message);
            }
            finally
            {
                logger.LeaveMethod();
            }
            var findEmpByExistingCode = this._customerRepository.Find(_ => _.CustomerCode == empCode).FirstOrDefault();
            if (findEmpByExistingCode != null)
                return true;
            return false;
        }

        public bool Delete(Expression<Func<Customer, bool>> predicated)
        {
            logger.EnterMethod();
            try
            {
                var emp = this._customerRepository.Get(predicated);
                if (emp != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._customerRepository.Delete(emp);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Success delete customer with Id: [" + emp.Id + "]");
                    return true;
                }
                else
                {
                    logger.Info("Couldn't found customer fit predicated");
                    return false;
                }
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

        public IQueryable<Customer> GetCustomerByPage(int index, bool inuse = true)
        {
            logger.EnterMethod();
            try
            {
                var countAllEmp = this._customerRepository.Count();
                if (countAllEmp <= this.empPerPage)
                {
                    logger.Info("All servicesfit a page. Get all services to a page");
                    return this.GetAll();
                }
                else
                {
                    var numberOfPages = CountTotalPages();
                    logger.Info("Found: [" + countAllEmp.ToString() + "] services fit [" + numberOfPages.ToString() + "] pages");
                    if (index > numberOfPages)
                    {
                        logger.Error("Can't access page: [" + index.ToString() + "]. Out of range paging");
                        // Get all services, skip [servicesPerPage * (numberOfPages - 1)] records and take servicesPerPage records after
                        if(inuse == true)
                            return (from customers in this._customerRepository.GetAll()
                                    select customers).OrderBy(_ => _.Id).Skip((numberOfPages - 1) * empPerPage).Take(empPerPage);
                        else
                            return (from customers in this._customerRepository.GetAll()
                                    select customers).OrderBy(_ => _.Id).Skip((numberOfPages - 1) * empPerPage).Take(empPerPage);
                    }
                    else
                    {
                        logger.Info("Getting query to showing customer of page: [" + index.ToString() + "] in [" + numberOfPages.ToString() + "] pages");
                        // Get all services, skip [index - 1 * servicesPerPage] records and take servicesPerPage records after
                        if(inuse == true)
                            return (from customers in this._customerRepository.GetAll()
                                    select customers).OrderBy(_ => _.Id).Skip((index - 1) * empPerPage).Take(empPerPage);
                        else
                            return (from customers in this._customerRepository.GetAll()
                                    select customers).OrderBy(_ => _.Id).Skip((index - 1) * empPerPage).Take(empPerPage);
                    }
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

        public int CountAllCustomer()
        {
            logger.EnterMethod();
            try
            {
                var count = this._customerRepository.Count();
                if (count > 0)
                {
                    logger.Info("Found: [" + count.ToString() + "] customers");
                    return count;
                }
                else
                {
                    logger.Info("Can't find any customer");
                    return 0;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return -1;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public int CountTotalPages()
        {
            logger.EnterMethod();
            try
            {
                var allPages = (int)(this.CountAllCustomer() / this.empPerPage);
                allPages = ((double)this.CountAllCustomer() / (double)this.empPerPage) != (allPages * 1.0) ? allPages += 1 : allPages;
                logger.Info("Calculate [" + allPages.ToString() + "] pages customer");
                return allPages;
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return -1;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public Customer GetLast()
        {
            logger.EnterMethod();
            try
            {
                var emp = this.GetAll().OrderByDescending(_ => _.Id).FirstOrDefault();
                if(emp != null)
                    logger.Info("Found customer with Id: [" + emp.Id +"]");
                return emp;
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
