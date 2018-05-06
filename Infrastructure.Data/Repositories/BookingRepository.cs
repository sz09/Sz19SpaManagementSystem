using Core.ObjectServices.Repositories;
using Core.ObjectServices.UnitOfWork;
using log4net;
using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Logging;
using System.Transactions;

namespace Infrastructure.Data.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        #region Attributes
        private readonly IRepository<Bills> _iBillRepositories;
        private readonly IUnitOfWork _iUnitOfWork;
        private static readonly ILog logger = LogManager.GetLogger(typeof(BookingRepository));
        private int _bookingPerPage;
        private int _defaultBookingPerPage = 20;
        #endregion
        #region Constructors
        public BookingRepository(IRepository<Bills> iBillRepositories, IUnitOfWork iUnitOfWork)
        {
            logger.EnterMethod();
            this._iBillRepositories = iBillRepositories;
            this._iUnitOfWork = iUnitOfWork; 
            logger.LeaveMethod();
        }
        #endregion
        #region Operations
        public bool Booking(Bills bill)
        {
            logger.EnterMethod();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    this._iBillRepositories.Add(bill);
                    transactionScope.Complete();
                }
                this._iUnitOfWork.Save();
                logger.Info("Inserted bill to database and save all changes");
                return true;
            }
            catch (Exception e)
            {
                logger.Error("Error: [" + e.Message +"]");
                return false;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public bool DeleteBooking(long id)
        {
            logger.EnterMethod();
            try
            {
                var bill = this._iBillRepositories.Get(_ => _.Id == id);
                if(bill != null)
                {
                    using(TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._iBillRepositories.Delete(bill);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Delete bill with Id: [" + id + "]");
                    return true;
                }
                else
                {
                    logger.Info("Can't find bill with Id: [" + id + "]");
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

        public IEnumerable<Bills> GetAll()
        {
            logger.EnterMethod();
            try
            {
                var allBills = this._iBillRepositories.GetAll().ToList();
                if (allBills != null)
                    logger.Info("Found [" + allBills.Count + "] bills");
                else
                    logger.Info("Can't find any bills");
                return allBills;
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

        public IEnumerable<Bills> GetAllBillPaid(bool isPaid = false)
        {
            logger.EnterMethod();
            try
            {
                var allBills = this._iBillRepositories.Find(_ => _.IsPaid == isPaid).ToList();
                if(allBills != null)
                    logger.Info("Found [" + allBills.Count + "] bills with paid is: [" + isPaid.ToString() + "]");
                else
                    logger.Info("Can't find any bills with paid is: [" + isPaid.ToString() + "]");
                return allBills;
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

        public IQueryable<Bills> GetBillPaging(int index)
        {
            logger.EnterMethod();
            try
            {
                int allPages = GetAllPages();
                if (index > allPages)
                {
                    logger.Info("Index is out of pages. Get booking in last page");
                    return (from booking in this._iBillRepositories.GetAll()
                            select booking).OrderBy(_ => _.Id)
                            .Skip((allPages - 1) * this._bookingPerPage)
                            .Take(this._bookingPerPage);
                }
                else
                {
                    logger.Info("Get booking in page [" + index + "]");
                    return (from booking in this._iBillRepositories.GetAll()
                            select booking).OrderBy(_ => _.Id)
                            .Skip((index - 1) * this._bookingPerPage)
                            .Take(this._bookingPerPage);
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

        public int GetAllPages()
        {
            logger.EnterMethod();
            try
            {
                int allPages = CountAllBookings() / this._bookingPerPage;
                logger.Info("Found [" + allPages + "] pages for booking");
                return allPages;
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
        public int CountAllBookings()
        {
            return this._iBillRepositories.Count();
        }

        public IQueryable<Bills> GetBillPaidPaging(int index, bool isPaid = false)
        {
            logger.EnterMethod();
            try
            {
                int allPages = GetAllPages();
                if (index > allPages)
                {
                    logger.Info("Index is out of pages. Get booking in last page");
                    return (from booking in this._iBillRepositories.GetAll()
                            where booking.IsPaid == isPaid
                            select booking).OrderBy(_ => _.Id)
                            .Skip((allPages - 1) * this._bookingPerPage)
                            .Take(this._bookingPerPage);
                }
                else
                {
                    logger.Info("Get booking in page [" + index + "]");
                    return (from booking in this._iBillRepositories.GetAll()
                            where booking.IsPaid == isPaid
                            select booking).OrderBy(_ => _.Id)
                            .Skip((index - 1) * this._bookingPerPage)
                            .Take(this._bookingPerPage);
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

        public bool UpdateBooking(Bills bill)
        {
            logger.EnterMethod();
            try
            {
                var booking = this._iBillRepositories.Get(_ => _.Id == bill.Id);
                if(booking != null)
                {
                    booking.BedId = bill.BedId;
                    booking.CustomerId = bill.CustomerId;
                    booking.IsPaid = bill.IsPaid;
                    booking.PeriodFrom = bill.PeriodFrom;
                    booking.PeriodTo = bill.PeriodTo;
                    booking.StaffId = bill.StaffId;
                    booking.TimePaid = bill.TimePaid;
                    booking.TotalCost = bill.TotalCost;
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._iBillRepositories.Update(booking);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Update booking [" + bill.Id + "] and save all changes");
                    return true;
                }
                else
                {
                    logger.Info("Can't find ");
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

        public bool UpdatePaid(long id, bool isPaid = true)
        {
            logger.EnterMethod();
            try
            {
                var booking = this._iBillRepositories.Get(id);
                if(booking != null)
                {
                    booking.IsPaid = isPaid;
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._iBillRepositories.Update(booking);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Update paid and save change for Id: [" + id + "]");    
                    return true;
                }
                else
                {
                    logger.Info("Can't find any booking for Id: [" + id + "]");
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
        #endregion
    }
}
