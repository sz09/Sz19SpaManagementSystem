using SPMS.ObjectModel.Entities;
using System.Linq;

namespace Service.Business.IServices
{
    /// <summary>
    /// IStaffServices defines class to implement in class inherited
    ///     Using interface to resolve issue about Employee
    ///         This service passes params into operations in IStaffRepository and retrive data
    /// </summary>
    public interface IStaffServices
    {
        /// <summary>
        /// Get Employee by Id
        /// </summary>
        /// <param name="staffId">Id of employee</param>
        /// <returns>
        /// Staff type
        ///     Otherwise, return null
        /// </returns>
        Staff Get(long staffId);
        /// <summary>
        /// Get a query type Staff
        ///     When using data in that query, it come to database and get all employees
        ///         Using lazy loading
        /// </summary>
        /// <returns>IQueryable</returns>
        IQueryable<Staff> GetAll();
        /// <summary>
        /// Get a query type Staff
        ///     When using data in that query, it come to database and get all employees inuse
        ///         Using lazy loading
        /// </summary>
        /// <param name="isInUse">Emp in use or not</param>
        /// <returns>IQueryable</returns>
        /// 
        IQueryable<Staff> GetAll(bool isInUse = true);
        /// <summary>
        /// Automatic create new code for employee
        /// </summary>
        /// <returns>Code of employee constant length</returns>
        string CreateNewCode();
        /// <summary>
        /// Paging feature
        ///     Using parcel page to get employees by page
        ///         Get query, lazy loading
        /// </summary>
        /// <param name="index">Index choosen</param>
        /// <returns>Query, lazy loading</returns>
        IQueryable<Staff> GetStaffByPage(int index, bool inuse = true);
        /// <summary>
        /// Get number of all employees existing in database
        /// </summary>
        /// <returns>Number of employees</returns>
        int CountAllStaff();
        /// <summary>
        /// Get all pages paging
        /// </summary>
        /// <returns>Total pages</returns>
        int CountTotalPages();
        /// <summary>
        /// Insert employee to database and save all changes
        /// </summary>
        /// <param name="emp">Type Staff by Entities</param>
        /// <returns>
        /// If insert success, return true
        ///     Otherwise, return false
        /// </returns>
        bool InsertEmployee(Staff emp);
        /// <summary>
        /// Insert employee to database and save all changes
        /// </summary>
        /// <param name="emp">Type Staff by Entities</param>
        /// <returns>
        /// If insert success, return true
        ///     Otherwise, return false
        /// </returns>
        bool UpdateEmployee(Staff emp);
        /// <summary>
        /// Update salary for an employee find by Id 
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="salary"></param>
        /// <returns></returns>
        bool UpdateSalaryForStaff(long empId, decimal salary);
        /// <summary>
        /// Delete employee
        ///     Save all changes
        /// </summary>
        /// <param name="empId">Id of employee</param>
        /// <returns>
        /// If delete success, return true
        ///     Otherwise, return false
        /// </returns>
        bool DeleteEmployee(int empId);
        /// <summary>
        /// Get last employee in database
        /// </summary>
        /// <returns></returns>
        Staff GetLast();
    }
}
