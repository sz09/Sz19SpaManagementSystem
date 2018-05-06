using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Business.IServices
{
    public interface ISalaryServices
    {
        /// <summary>
        /// Get all salaries in database fit employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IQueryable<Salary> GetSalariesForEmployee(long id);
        /// <summary>
        /// Get all salaries in database fit employee
        ///     Get record fit salaries per page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IQueryable<Salary> GetSalariesForStaffPreviewNearest(long id);
        /// <summary>
        /// Get all salaries in database fit employee in month
        ///     Get record fit salaries per page
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        IQueryable<Salary> GetSalariesForStaffPreviewNearest(long id, DateTime date);
        /// <summary>
        /// Get count records for Salary in database for employee
        /// </summary>
        /// <param name="id">Id of employee</param>
        /// <returns></returns>
        int TotalSalariesInPast(long id);
        /// <summary>
        /// Get values of set salary showing per page
        /// </summary>
        /// <returns></returns>
        int GetSalariesInPage();
        /// <summary>
        /// Count total times attend of an employee in database
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        int AttendanceInMonth(long empId, DateTime date);
        /// <summary>
        /// Update value of employee for a time to value
        /// </summary>
        /// <param name="empId">Id of employee</param>
        /// <param name="date">Time paid</param>
        /// <param name="value">Value to set</param>
        /// <returns></returns>
        bool UpdatePaid(long empId, DateTime date, bool value);
        /// <summary>
        /// Update value of salary
        /// </summary>
        /// <param name="salary"></param>
        /// <returns></returns>
        bool UpdatePaid(Salary salary);
        /// <summary>
        /// Get all salaries paid in month
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        List<Salary> GetAllSalariesPaidInMonth(DateTime dateTime);
        /// <summary>
        /// Get all month paid salary for employee
        /// </summary>
        /// <returns></returns>
        List<DateTime> GetCollectionMonthPaid();
        /// <summary>
        /// Get all salaries for employee in a month 
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        IQueryable<Salary> GetAllSalariesForEmployee(long empId, int month = -1, int year = -1);
    }
}
