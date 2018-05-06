using Core.ObjectServices.Repositories;
using log4net;
using Infrastructure.Logging;
using Service.Business.IServices;
using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Business.Services
{
    public class NotificationServices : INotificationServices
    {
        #region Attributes
        private readonly INotificationRepository _iNotificationRepositories;
        private static readonly ILog logger = LogManager.GetLogger(typeof(NotificationServices));
        #endregion
        #region Constructors
        public NotificationServices(INotificationRepository iNotificationRepositories)
        {
            logger.EnterMethod();
            this._iNotificationRepositories = iNotificationRepositories;
            logger.LeaveMethod();
        }
        #endregion
        #region Operations
        public Notification GetNotification(long Id)
        {
            logger.EnterMethod();
            try
            {
                return this._iNotificationRepositories.GetNotification(Id);
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

        public IEnumerable<Notification> GetNotificationForAccount(long accountId)
        {
            logger.EnterMethod();
            try
            {
                return this._iNotificationRepositories.GetNotificationForAccount(accountId);
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

        public IEnumerable<NotificationDetail> GetNotificationDetail(long notificationId)
        {
            logger.EnterMethod();
            try
            {
                return this._iNotificationRepositories.GetNotificationDetail(notificationId);
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

        public bool ReadNotification(long Id)
        {
            logger.EnterMethod();
            try
            {
                return this._iNotificationRepositories.ReadNotification(Id);
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


        public int CountNotificationForAccount(long accountId, bool isRead = false)
        {
            logger.EnterMethod();
            try
            {
                return this._iNotificationRepositories.CountNotificationForAccount(accountId);
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
        #endregion
    }
}
