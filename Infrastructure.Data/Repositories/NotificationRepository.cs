using Core.ObjectServices.Repositories;
using log4net;
using SPMS.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Logging;
using System.Transactions;
using Core.ObjectServices.UnitOfWork;
namespace Infrastructure.Data.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        #region Attributes
        private static readonly ILog logger = LogManager.GetLogger(typeof(NotificationRepository));
        private readonly IRepository<Notification> _notificationRepositories;
        private readonly IRepository<NotificationDetail> _notificationDetailRepositories;
        private readonly IUnitOfWork _iUnitOfWork;
        #endregion
        #region Constructors
        public NotificationRepository(IRepository<Notification> notificationRepositories, IRepository<NotificationDetail> notificationDetailRepositories, IUnitOfWork iUnitOfWork)
        {
            this._iUnitOfWork = iUnitOfWork;
            this._notificationDetailRepositories = notificationDetailRepositories;
            this._notificationRepositories = notificationRepositories;
        }
        #endregion
        #region Operations
        public Notification GetNotification(long Id)
        {
            logger.EnterMethod();
            try
            {
                var notification = this._notificationRepositories.Get(_=>_.Id == Id);
                if (notification != null)
                    logger.Info("Found notification for Id: [" + Id + "]");
                else
                    logger.Info("Can't find any notification with Id: [" + Id + "]");
                return notification;
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
                var notificaions = this._notificationRepositories.Find(_ => _.ForAccountId == accountId);
                if (notificaions.ToList() != null)
                {
                    logger.Info("Found [" + notificaions.ToList().Count + "] notifications for Account with Id: [" + accountId + "]");
                    return notificaions.ToList();
                }
                else
                {
                    logger.Info("Can't find any notification for Account with Id: [" + accountId + "]");
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

        public IEnumerable<Notification> GetNotificationForAccount(long accountId, bool isRead = false)
        {
            logger.EnterMethod();
            try
            {
                var notifications = new List<Notification>();
                if (isRead == false)
                {
                    var notificationNotReadYet = this._notificationRepositories.Find(_ => _.ForAccountId == accountId &&
                                                                           _.IsRead == false);
                    if(notificationNotReadYet.ToList() != null)
                    {
                        notifications = notificationNotReadYet.ToList();
                        logger.Info("Found [" + notifications.Count + "] notifications for account with Id: [" + accountId + "] not read yet");
                    }
                    else
                    {
                        notifications = null;
                        logger.Info("Account: [" + accountId + "] read all notifications");
                    }
                }
                else
                {
                    var allNnotifications = this._notificationRepositories.Find(_ => _.ForAccountId == accountId);
                    if (allNnotifications.ToList() != null)
                    {
                        notifications = allNnotifications.ToList();
                        logger.Info("Found [" + notifications.Count + "] notifications for account with Id: [" + accountId + "]");
                    }
                    else
                    {
                        notifications = null;
                        logger.Info("Account: [" + accountId + "] hasn't any notifications");
                    }
                }
                return notifications;
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

        public int CountNotificationForAccount(long accountId, bool isRead = false)
        {
            logger.EnterMethod();
            try
            {
                int count = 0;
                if (isRead == false)
                {
                    var notificationNotReadYet = this._notificationRepositories.Find(_ => _.ForAccountId == accountId &&
                                                                           _.IsRead == false).ToList();
                    if (notificationNotReadYet != null)
                    {
                        count = notificationNotReadYet.Count;
                        logger.Info("Found [" + count + "] notifications for account with Id: [" + accountId + "] not read yet");
                    }
                    else
                    {
                        count = 0;
                        logger.Info("Account: [" + accountId + "] read all notifications");
                    }
                }
                else
                {
                    var allNnotifications = this._notificationRepositories.Find(_ => _.ForAccountId == accountId).ToList();
                    if (allNnotifications != null)
                    {
                        count = allNnotifications.Count;
                        logger.Info("Found [" + count + "] notifications for account with Id: [" + accountId + "]");
                    }
                    else
                    {
                        count = 0;
                        logger.Info("Account: [" + accountId + "] hasn't any notifications");
                    }
                }
                return count;
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

        public IEnumerable<NotificationDetail> GetNotificationDetail(long notificationId)
        {
            logger.EnterMethod();
            try
            {
                var notificationDetails = this._notificationDetailRepositories.Find(_ => _.NotificationId == notificationId).ToList();
                if (notificationDetails != null)
                {
                    logger.Info("Found [" + notificationDetails.ToList().Count + "] details for notification with Id:[" + notificationId + "]");
                    return notificationDetails;
                }
                else
                {
                    logger.Info("Can't find any detail for notification with Id:[" + notificationId + "]");
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

        public bool CreateNewNotification(Notification notification)
        {
            logger.EnterMethod();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    this._notificationRepositories.Add(notification);
                    transactionScope.Complete();
                }
                this._iUnitOfWork.Save();
                logger.Info("Insert Notification to database and save all changes success");
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

        public bool CreateNewNotificationDetail(NotificationDetail notificationDetail)
        {
            logger.EnterMethod();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    this._notificationDetailRepositories.Add(notificationDetail);
                    transactionScope.Complete();
                }
                this._iUnitOfWork.Save();
                logger.Info("Insert NotificationDetail to database and save all changes success");
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

        public bool UpdateNotification(Notification notification)
        {
            logger.EnterMethod();
            try
            {
                var notificationGet = this._notificationRepositories.Get(_ => _.Id == notification.Id);
                if (notificationGet != null)
                {
                    notificationGet.Details = notification.Details;
                    notificationGet.ForAccountId = notification.ForAccountId;
                    notificationGet.IsRead = notification.IsRead;
                    notificationGet.Summary = notification.Summary;
                    notificationGet.Time = notification.Time;

                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._notificationRepositories.Update(notificationGet);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Found Notification with Id: [" + notification.Id + "], update and save all changes success");
                    return true;
                }
                else
                {
                    logger.Info("Can't find any Notification with Id: [" + notification.Id + "]");
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

        public bool UpdateNotificationDetail(NotificationDetail notificationDetail)
        {
            logger.EnterMethod();
            try
            {
                var notificationDetailGet = this._notificationDetailRepositories.Get(_ => _.Id == notificationDetail.Id);
                if (notificationDetailGet != null)
                {
                    notificationDetailGet.Details = notificationDetail.Details;
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        this._notificationDetailRepositories.Update(notificationDetailGet);
                        transactionScope.Complete();
                    }
                    this._iUnitOfWork.Save();
                    logger.Info("Found NotificationDetail with Id: [" + notificationDetail.Id + "], update and save all changes success");
                    return true;
                }
                else
                {
                    logger.Info("Can't find any NotificationDetail with Id: [" + notificationDetail.Id + "]");
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

        public bool ReadNotification(long Id)
        {
            logger.EnterMethod();
            try
            {
                var notificationGet = this._notificationRepositories.Get(_ => _.Id == Id);
                if (notificationGet != null)
                {
                    if (notificationGet.IsRead == true)
                    {
                        logger.Info("Notification has been read yet. No need update");
                        return false;
                    }
                    else
                    {
                        notificationGet.IsRead = true;
                        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                        {
                            this._notificationRepositories.Update(notificationGet);
                            transactionScope.Complete();
                        }
                        this._iUnitOfWork.Save();
                        logger.Info("Update notification with Id: [" + Id + "] to read");
                        return true;
                    }
                }
                else
                {
                    logger.Info("Can't find any notification with Id: [" + Id + "]");
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
