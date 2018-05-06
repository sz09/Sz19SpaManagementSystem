using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.ObjectServices.Repositories
{
    public interface IStaffRepository
    {
        bool InsertStaff(Staff staff);
        bool UpdateStaff(Staff staff);
        bool DeleteStaff(int staffId);
        bool DeleteStaff(params object[] keyValues);
        bool Delete(IEnumerable<Staff> listStaff);
        bool Delete(Expression<Func<Staff, bool>> predicated);
        Staff Get(params object[] keyValues);
        Staff Get(long staffId);
        Staff GetLast();
        Staff Get(Expression<Func<Staff, bool>> predicated);
        Staff Get(Expression<Func<Staff, bool>> predicated, params Expression<Func<Staff, object>>[] includes);
        IQueryable<Staff> GetAll();
        IQueryable<Staff> GetAll(bool isInUse = true);
        IQueryable<Staff> GetAll(params Expression<Func<Staff, object>>[] includes);
        IQueryable<Staff> Find(Expression<Func<Staff, bool>> predicated);
        IQueryable<Staff> Find(Expression<Func<Staff, bool>> predicated, params Expression<Func<Staff, object>>[] includes);
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


    }
}
