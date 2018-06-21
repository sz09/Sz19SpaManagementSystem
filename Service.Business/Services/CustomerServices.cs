using Core.ObjectServices.Repositories;
using log4net;
using Service.Business.IServices;
using SPMS.ObjectModel.Entities;
using System;
using System.Linq;
using Infrastructure.Logging;

namespace Service.Business.Services
{
    /// <summary>
    /// CustomerServices is implemented ICustomerServices
    /// Services Tier for Customer
    /// </summary>
    public class CustomerServices: ICustomerServices
    {
        #region Attributes
        private readonly ICustomerRepository _iCustomerRepository;
        private static readonly ILog logger = LogManager.GetLogger(typeof(CustomerServices));
        #endregion

        #region Constructors
        public CustomerServices(ICustomerRepository _iCustomerRepository)
        {
            logger.EnterMethod();
            this._iCustomerRepository = _iCustomerRepository;
            logger.Info("Success setting values for attributes");
            logger.LeaveMethod();
        }
        #endregion

        #region Operations
        /// <summary>
        /// Get Customer by customerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer Get(long customerId)
        {
            logger.EnterMethod();
            try
            {
                var cus = this._iCustomerRepository.Get(customerId);
                if (cus != null)
                {
                    logger.Info("Found customer: [" + cus.LastMiddle + " " +  cus.FirstName + "] by Id: [" + customerId.ToString() + "]");
                    return cus;
                }
                else
                {
                    logger.Info("Can't find customer by Id: [" + customerId.ToString() + "]");
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

        public IQueryable<Customer> GetAll()
        {
            logger.EnterMethod();
            try
            {
                return this._iCustomerRepository.GetAll();
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
        

        public string CreateNewCode()
        {
            logger.EnterMethod();
            try
            {
                return this._iCustomerRepository.CreateNewCode();
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


        public IQueryable<Customer> GetCustomerByPage(int index, bool inuse = true)
        {
            logger.EnterMethod();
            try
            {
                return this._iCustomerRepository.GetCustomerByPage(index, inuse);
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
                return this._iCustomerRepository.CountAllCustomer();
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
                return this._iCustomerRepository.CountTotalPages();
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
        
        public long InsertCustomerReturnId(Customer cus)
        {
            logger.EnterMethod();
            try
            {
                return this._iCustomerRepository.InsertCustomerReturnId(cus);
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
        public bool InsertCustomer(Customer cus)
        {
            logger.EnterMethod();
            try
            {
                return this._iCustomerRepository.InsertCustomer(cus);
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


        public bool DeleteCustomer(int cusId)
        {
            logger.EnterMethod();
            try
            {
                return this._iCustomerRepository.DeleteCustomer(cusId);
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

        public Customer GetLast()
        {
            logger.EnterMethod();
            try
            {
                return this._iCustomerRepository.GetLast();
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

        public bool UpdateCustomer(Customer cus)
        {
            logger.EnterMethod();
            try
            {
                return this._iCustomerRepository.UpdateCustomer(cus);
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
        
        #endregion
    }
}
