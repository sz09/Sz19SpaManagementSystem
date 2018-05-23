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
    /// StaffServices is implemented IStaffServices
    /// Services Tier for Staff
    /// </summary>
    public class StaffServices: IStaffServices
    {
        #region Attributes
        private readonly IStaffRepository _iStaffRepository;
        private static readonly ILog logger = LogManager.GetLogger(typeof(StaffServices));
        #endregion

        #region Constructors
        public StaffServices(IStaffRepository _iStaffRepository)
        {
            logger.EnterMethod();
            this._iStaffRepository = _iStaffRepository;
            logger.Info("Success setting values for attributes");
            logger.LeaveMethod();
        }
        #endregion

        #region Operations
        /// <summary>
        /// Get Staff by staffId
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public Staff Get(long staffId)
        {
            logger.EnterMethod();
            try
            {
                var emp = this._iStaffRepository.Get(staffId);
                if (emp != null)
                {
                    logger.Info("Found employee: [" + emp.LastMiddle + " " +  emp.FirstName + "] by Id: [" + staffId.ToString() + "]");
                    return emp;
                }
                else
                {
                    logger.Info("Can't find employee by Id: [" + staffId.ToString() + "]");
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

        public IQueryable<Staff> GetAll()
        {
            logger.EnterMethod();
            try
            {
                return this._iStaffRepository.GetAll();
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

        public IQueryable<Staff> GetAll(bool isInUse = true)
        {
            logger.EnterMethod();
            try
            {

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
            return this._iStaffRepository.GetAll(isInUse);
        }

        public string CreateNewCode()
        {
            logger.EnterMethod();
            try
            {
                return this._iStaffRepository.CreateNewCode();
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


        public IQueryable<Staff> GetStaffByPage(int index, bool inuse = true)
        {
            logger.EnterMethod();
            try
            {
                return this._iStaffRepository.GetStaffByPage(index, inuse);
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

        public int CountAllStaff()
        {
            logger.EnterMethod();
            try
            {
                return this._iStaffRepository.CountAllStaff();
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
                return this._iStaffRepository.CountTotalPages();
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


        public bool InsertEmployee(Staff emp)
        {
            logger.EnterMethod();
            try
            {
                return this._iStaffRepository.InsertStaff(emp);
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


        public bool DeleteEmployee(int empId)
        {
            logger.EnterMethod();
            try
            {
                return this._iStaffRepository.DeleteStaff(empId);
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

        public Staff GetLast()
        {
            logger.EnterMethod();
            try
            {
                return this._iStaffRepository.GetLast();
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

        public bool UpdateEmployee(Staff emp)
        {
            logger.EnterMethod();
            try
            {
                return this._iStaffRepository.UpdateStaff(emp);
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

        public bool UpdateSalaryForStaff(long empId, decimal salary)
        {
            logger.EnterMethod();
            try
            {
                return this._iStaffRepository.UpdateSalaryForStaff(empId, salary);
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
