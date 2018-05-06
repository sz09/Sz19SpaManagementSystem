using Core.ObjectServices.Repositories;
using Service.Business.IServices;
using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Infrastructure.Logging;

namespace Service.Business.Services
{
    public class BookingServices : IBookingServices
    {
        #region Attributes
        private readonly IBookingRepository _iBookingRepositories;
        private readonly ILog logger = LogManager.GetLogger(typeof(BookingServices));
        #endregion
        #region Constructors
        public BookingServices(IBookingRepository iBookingRepositories)
        {
            this._iBookingRepositories = iBookingRepositories;
        }
        #endregion
        public bool Booking(Bills bill)
        {
            logger.EnterMethod();
            try
            {
                return this._iBookingRepositories.Booking(bill);
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

        public bool DeleteBooking(long id)
        {
            logger.EnterMethod();
            try
            {
                return this._iBookingRepositories.DeleteBooking(id);
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
                return this._iBookingRepositories.GetAll();
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
                return this._iBookingRepositories.GetAllBillPaid(isPaid);
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
                return this.GetBillPaging(index);
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

        public IQueryable<Bills> GetBillPaidPaging(int index, bool isPaid = false)
        {
            logger.EnterMethod();
            try
            {
                return this._iBookingRepositories.GetBillPaidPaging(index, isPaid);
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
                return this._iBookingRepositories.UpdateBooking(bill);
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
                return this.UpdatePaid(id, isPaid);
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
    }
}
