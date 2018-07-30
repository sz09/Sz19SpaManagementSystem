using SPMS.ObjectModel.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core.ObjectServices.Repositories
{
    public interface IBookingRepository
    {
        /// <summary>
        /// Get all bill in database
        /// </summary>
        /// <returns>
        /// Collection of Bill if found
        ///     Otherwise, return null
        /// </returns>
        IEnumerable<Bills> GetAll();
        /// <summary>
        /// Get all bill in database match isPaid passed
        /// </summary>
        /// <param name="isPaid">Check paid for bill, default is un-padid</param>
        /// <returns>
        /// Collection of Bill if found
        ///     Otherwise, return null
        /// </returns>
        IEnumerable<Bills> GetAllBillPaid(bool isPaid = false);
        /// <summary>
        /// Get a query for paging bill
        /// </summary>
        /// <param name="index">Page want get</param>
        /// <returns>
        /// IQueryable<Bill>, lazy loading
        /// </returns>
        IQueryable<Bills> GetBillPaging(int index);
        /// <summary>
        /// Get a query for paging bill
        /// </summary>
        /// <param name="index">Page want get</param>
        /// <returns>
        /// IQueryable<Bill>, lazy loading
        /// </returns>
        IQueryable<Bills> GetBillBed(long bedId, bool isPaid = false);
        /// <summary>
        /// Get a query for paging bill
        /// </summary>
        /// <param name="index">Page want get</param>
        /// <returns>
        /// IQueryable<Bill>, lazy loading
        /// </returns>
        Bills GetBill(long id);
        /// <summary>
        /// Get a query for paging bill
        /// </summary>
        /// <param name="index">Page want get</param>
        /// <param name="isPaid">Check paid for bill, default is un-padid</param>
        /// <returns>
        /// IQueryable<Bill>, lazy loading
        /// </returns>
        IQueryable<Bills> GetBillPaidPaging(int index, bool isPaid = false);
        /// <summary>
        /// Create a booking
        ///     Keeping a bed can't use in time period and information of a customer
        /// </summary>
        /// <returns>
        /// If inserted and save all changes, return true
        ///     Otherwise, return false
        /// </returns>
        bool Booking(Bills bill);
        /// <summary>
        /// Paid for a bill
        /// </summary>
        /// <param name="id">Id of bill</param>
        /// <param name="isPaid">Check paid for bill, default is paid</param>
        /// <returns>
        /// If found bill, update and save all change, return true
        ///     Otherwise, return false
        /// </returns>
        bool UpdatePaid(long id, bool isPaid = true);
        /// <summary>
        /// Update a bil
        /// </summary>
        /// <param name="bill">Bill entities</param>
        /// <returns>
        /// Updated and save all changes, return true
        ///     Otherwise, return false
        /// </returns>
        bool UpdateBooking(Bills bill);
        /// <summary>
        /// Delete booking and save all changes
        /// </summary>
        /// <param name="id">Id of bill</param>
        /// <returns>
        /// If deleted and save all changes, return true
        ///     Otherwise, return false
        /// </returns>
        bool DeleteBooking(long id);
    }
}
