using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.ObjectServices.Repositories
{
    public interface ICustomerRepository
    {
        bool InsertCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(int customerId);
        bool DeleteCustomer(params object[] keyValues);
        bool Delete(IEnumerable<Customer> listCustomer);
        bool Delete(Expression<Func<Customer, bool>> predicated);
        Customer Get(params object[] keyValues);
        Customer Get(long customerId);
        Customer GetLast();
        Customer Get(Expression<Func<Customer, bool>> predicated);
        Customer Get(Expression<Func<Customer, bool>> predicated, params Expression<Func<Customer, object>>[] includes);
        IQueryable<Customer> GetAll();
        IQueryable<Customer> GetAll(params Expression<Func<Customer, object>>[] includes);
        IQueryable<Customer> Find(Expression<Func<Customer, bool>> predicated);
        IQueryable<Customer> Find(Expression<Func<Customer, bool>> predicated, params Expression<Func<Customer, object>>[] includes);
        string CreateNewCode();
        /// <summary>
        /// Paging feature
        ///     Using parcel page to get employees by page
        ///         Get query, lazy loading
        /// </summary>
        /// <param name="index">Index choosen</param>
        /// <returns>Query, lazy loading</returns>
        IQueryable<Customer> GetCustomerByPage(int index, bool inuse = true);
        /// <summary>
        /// Get number of all employees existing in database
        /// </summary>
        /// <returns>Number of employees</returns>
        int CountAllCustomer();
        /// <summary>
        /// Get all pages paging
        /// </summary>
        /// <returns>Total pages</returns>
        int CountTotalPages();

        
    }
}
