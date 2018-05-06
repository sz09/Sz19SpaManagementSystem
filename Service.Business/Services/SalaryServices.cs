using Core.ObjectServices.Repositories;
using log4net;
using Service.Business.IServices;
using SPMS.ObjectModel.Entities;
using System;
using System.Linq;
using Infrastructure.Logging;
using System.Collections.Generic;

namespace Service.Business.Services
{
    public class SalaryServices : ISalaryServices
    {
        #region Attributes
        private readonly ISalaryRepository _iSalaryRepositories;
        private readonly ILog logger = LogManager.GetLogger(typeof(SalaryServices));
        #endregion

        #region Constructors
        public SalaryServices(ISalaryRepository iSalaryRepositories)
        {
            this._iSalaryRepositories = iSalaryRepositories;
        }
        #endregion

        #region Operations
        public IQueryable<Salary> GetSalariesForEmployee(long id)
        {
            logger.EnterMethod();
            try
            {
                return this._iSalaryRepositories.GetSalariesForStaff(id);
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

        public IQueryable<Salary> GetSalariesForStaffPreviewNearest(long id)
        {
            logger.EnterMethod();
            try
            {
                return this._iSalaryRepositories.GetSalariesForStaffPreviewNearest(id);
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

        public IQueryable<Salary> GetSalariesForStaffPreviewNearest(long id, DateTime date)
        {
            logger.EnterMethod();
            try
            {
                return this._iSalaryRepositories.GetSalariesForStaffPreviewNearest(id, date);
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

        public int TotalSalariesInPast(long id)
        {
            logger.EnterMethod();
            try
            {
                return this._iSalaryRepositories.TotalSalariesInPast(id);
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message + "]");
                return 1;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public int GetSalariesInPage()
        {
            logger.EnterMethod();
            try
            {
                return this._iSalaryRepositories.GetSalariesInPage();
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


        public int AttendanceInMonth(long empId, DateTime date)
        {
            logger.EnterMethod();
            try
            {
                return this._iSalaryRepositories.AttendanceInMonth(empId, date);
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

        public bool UpdatePaid(long empId, DateTime date, bool value)
        {
            logger.EnterMethod();
            try
            {
                return this._iSalaryRepositories.UpdatePaid(empId, date, value);
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

        public bool UpdatePaid(Salary salary)
        {
            logger.EnterMethod();
            try
            {
                return this._iSalaryRepositories.UpdatePaid(salary);
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

        public List<Salary> GetAllSalariesPaidInMonth(DateTime dateTime)
        {
            logger.EnterMethod();
            try
            {

                return this._iSalaryRepositories.GetAllSalariesPaidInMonth(dateTime);
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

        public List<DateTime> GetCollectionMonthPaid()
        {
            logger.EnterMethod();
            try
            {
                return this._iSalaryRepositories.GetCollectionMonthPaid();
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

        public IQueryable<Salary> GetAllSalariesForEmployee(long empId, int month = -1, int year = -1)
        {
            logger.EnterMethod();
            try
            {
                return this._iSalaryRepositories.GetAllSalariesForEmployee(empId, month, year);
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
