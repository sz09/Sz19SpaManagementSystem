using Core.ObjectServices.Repositories;
using Core.ObjectServices.UnitOfWork;
using SPMS.ObjectModel.Entities;
using System;
using System.Linq;
using log4net;
using Infrastructure.Logging;
using System.Transactions;
using System.Collections.Generic;

namespace Infrastructure.Data.Repositories
{
    public class SalaryRepository : ISalaryRepository
    {
        #region Attributes
        private readonly IRepository<Salary> _salaryRepositories;
        private readonly IRepository<Attendance> _attendanceRepositories;
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly ILog logger = LogManager.GetLogger(typeof(SalaryRepository));
        private readonly int previewNearestValues;
        private readonly int defaultPreviewNearestValues = 5;
        #endregion

        #region Constructors
        public SalaryRepository(IRepository<Salary> salaryRepositories, IRepository<Attendance> attendanceRepositories, IUnitOfWork iUnitOfWork)
        {
            logger.EnterMethod();
            this._salaryRepositories = salaryRepositories;
            this._attendanceRepositories = attendanceRepositories;
            this._iUnitOfWork = iUnitOfWork;
            try
            {
                this.previewNearestValues = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["previewNearest"]);
                logger.Info("Success setting value to servicesPerPage attribute value: [" + this.previewNearestValues.ToString() + "]");
            }
            catch (Exception ex)
            {
                this.previewNearestValues = defaultPreviewNearestValues;
                logger.Error("Error:[" + ex.Message + "]. Setting default value: [" + this.previewNearestValues.ToString() + "]");
            }
            logger.Info("Success set value for attributes");
            logger.LeaveMethod();
        }
        #endregion

        #region Operations
        public IQueryable<Salary> GetSalariesForStaff(long id)
        {
            logger.EnterMethod();
            try
            {
                return this._salaryRepositories.Find(_ => _.StaffId == id);
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
                return this._salaryRepositories.Find(_ => _.StaffId == id).Take(this.previewNearestValues);
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
                return this._salaryRepositories.Find(_ => _.StaffId == id &&
                                                _.Time.Month == date.Month &&
                                                _.Time.Year == date.Year).
                                                Take(this.previewNearestValues);
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
                var salaries = this.GetSalariesForStaff(id);
                if (salaries.ToList() != null)
                {
                    int count = salaries.ToList().Count;
                    logger.Info("Found: [" + count + "] values salaries of employee with Id: [" + id + "]");
                    return count;
                }
                else
                {
                    logger.Info("Can't find any salaries for emloyee with Id: [" + id + "]");
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

        public int GetSalariesInPage()
        {
            logger.EnterMethod();
            try
            {
                return this.previewNearestValues;
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

        public int AttendanceInMonth(long empId, DateTime date)
        {

            logger.EnterMethod();
            try
            {
                var attendances = this._attendanceRepositories.Find(_ => _.Time.Month == date.Month &&
                                                           _.Time.Year == date.Year &&
                                                           _.StaffId == empId);
                int count = 0;
                if (attendances.ToList().Count > 0)
                {
                    count = attendances.ToList().Count;
                    logger.Info("Found [" + count + "] times attendance for employee with Id: [" + empId + "]");
                    return count;
                }
                else
                {
                    logger.Info("Can't found attendace for employee with Id: [" + empId + "]");
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

        public bool UpdatePaid(long empId, DateTime date, bool value)
        {
            logger.EnterMethod();
            try
            {
                var empSalaryInMonth = this._salaryRepositories.Get(_ => _.StaffId == empId &&
                                                                           _.Time.Year == date.Year &&
                                                                           _.Time.Month == date.Month);
                if (empSalaryInMonth != null)
                {

                    empSalaryInMonth.IsPaid = value;
                    empSalaryInMonth.TimePay = this._salaryRepositories.GetDateTimeServer();

                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._salaryRepositories.Update(empSalaryInMonth);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
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

        public bool UpdatePaid(Salary salary)
        {
            logger.EnterMethod();
            try
            {
                var paid = this._salaryRepositories.Get(_ => _.Id == salary.Id);
                if (paid != null)
                {
                    paid.IsPaid = salary.IsPaid;
                    paid.Time = salary.Time;
                    paid.TimePay = this._salaryRepositories.GetDateTimeServer();
                    paid.TotalSalary = salary.TotalSalary;
                    paid.Description = salary.Description;
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._salaryRepositories.Update(paid);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Update success salary Id: [" + salary.Id + "]");
                    return true;
                }
                else
                {
                    logger.Info("Update false salary Id: [" + salary.Id + "]");
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

        public bool CreateNewSalary(Salary salary)
        {
            logger.EnterMethod();
            try
            {
                salary.TimePay = this._salaryRepositories.GetDateTimeServer();
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    this._salaryRepositories.Add(salary);
                    transactionScope.Complete();
                }
                this._iUnitOfWork.Save();
                logger.Info("Create salary for [" + salary.Time.Month + "." + salary.Time.Year + "] at [" + salary.TimePay + "]");
                return true;
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
                var salaries = this._salaryRepositories.GetAll().Where(_ => (
                                                                  _.TimePay.HasValue && 
                                                                  _.TimePay.Value.Month == dateTime.Month) &&
                                                                  _.TimePay.Value.Year == dateTime.Year
                                                                  ).ToList();
                if (salaries != null)
                {
                    logger.Info("Found " + salaries.Count() + " salaries paid in [" + dateTime.Month + "-" + dateTime.Year + "]");
                    return salaries;
                }
                else
                {
                    logger.Info("Can't found any salaries paid in [" + dateTime.Month + "-" + dateTime.Year + "]");
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

        public List<DateTime> GetCollectionMonthPaid()
        {
            logger.EnterMethod();
            try
            {
                var times = (from _ in this._salaryRepositories.GetAll()
                                select _.Time).ToList();

                var dateTimes = times.
                                Select (_=> new DateTime(_.Year, _.Month, 1)).
                                Distinct().
                                ToList();
                if (dateTimes != null)
                {
                    logger.Info("Found " + dateTimes.Count + " month paid salaries");
                    return dateTimes;
                }
                else
                {
                    logger.Info("Can't find any month paid salaries");
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

        public IQueryable<Salary> GetAllSalariesForEmployee(long empId, int month = -1, int year = -1)
        {
            logger.EnterMethod();
            try
            {
                var salaries = (from salary in this._salaryRepositories.GetAll()
                               where salary.StaffId == empId &&
                                    salary.Time.Month == month &&
                                     salary.Time.Year == year
                               select salary);
                if (salaries != null && salaries.ToList().Count > 0)
                {
                    logger.Info("Found " + salaries.ToList().Count + " salaries for employee with Id: [" + empId + "] in [" + month + " - " + year +  "]");
                    return salaries;
                }
                else
                {
                    logger.Info("Can't find any salary for employee with Id: [" + empId + "] in [" + month + " - " + year + "]");
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
        #endregion
    }
}
