using SPMS.ObjectModel.Entities;
using System.Linq;

namespace Service.Business.IServices
{
    /// <summary>
    /// ICustomerServices defines class to implement in class inherited
    ///     Using interface to resolve issue about Employee
    ///         This service passes params into operations in ICustomerRepository and retrive data
    /// </summary>
    public interface ICustomerServices
    {
        /// <summary>
        /// Get Employee by Id
        /// </summary>
        /// <param name="customerId">Id of customer</param>
        /// <returns>
        /// Customer type
        ///     Otherwise, return null
        /// </returns>
        Customer Get(long customerId);
        /// <summary>
        /// Get a query type Customer
        ///     When using data in that query, it come to database and get all customers
        ///         Using lazy loading
        /// </summary>
        /// <returns>IQueryable</returns>
        IQueryable<Customer> GetAll();
        /// <summary>
        /// Automatic create new code for customer
        /// </summary>
        /// <returns>Code of customer constant length</returns>
        string CreateNewCode();
        /// <summary>
        /// Paging feature
        ///     Using parcel page to get customers by page
        ///         Get query, lazy loading
        /// </summary>
        /// <param name="index">Index choosen</param>
        /// <returns>Query, lazy loading</returns>
        IQueryable<Customer> GetCustomerByPage(int index, bool inuse = true);
        /// <summary>
        /// Get number of all customers existing in database
        /// </summary>
        /// <returns>Number of customers</returns>
        int CountAllCustomer();
        /// <summary>
        /// Get all pages paging
        /// </summary>
        /// <returns>Total pages</returns>
        int CountTotalPages();
        /// <summary>
        /// Insert customer to database and save all changes
        /// </summary>
        /// <param name="cus">Type Customer by Entities</param>
        /// <returns>
        /// If insert success, return true
        ///     Otherwise, return false
        /// </returns>
        bool InsertEmployee(Customer cus);
        /// <summary>
        /// Insert customer to database and save all changes
        /// </summary>
        /// <param name="cus">Type Customer by Entities</param>
        /// <returns>
        /// If insert success, return true
        ///     Otherwise, return false
        /// </returns>
        bool UpdateEmployee(Customer cus);
        /// <summary>
        /// Delete customer
        ///     Save all changes
        /// </summary>
        /// <param name="cusId">Id of customer</param>
        /// <returns>
        /// If delete success, return true
        ///     Otherwise, return false
        /// </returns>
        bool DeleteEmployee(int cusId);
        /// <summary>
        /// Get last customer in database
        /// </summary>
        /// <returns></returns>
        Customer GetLast();
    }
}
