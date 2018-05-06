using SPMS.ObjectModel.Entities;
using System.Collections.Generic;

namespace Service.Business.IServices
{
    public interface INotificationServices
    {
        /// <summary>
        /// Get notification by Id
        /// </summary>
        /// <param name="Id">Id of Notification</param>
        /// <returns>
        /// Notificaiton if found.
        ///     Otherwise, return null.
        /// </returns>
        Notification GetNotification(long Id);
        /// <summary>
        /// Get collection of notification fit accountId
        /// </summary>
        /// <param name="accountId">Id of account</param>
        /// <returns>
        /// Collection of Notification if found.
        ///     Otherwise, return null
        /// </returns>
        IEnumerable<Notification> GetNotificationForAccount(long accountId);
        /// <summary>
        /// Get detail of notification
        ///     Collection if 
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        IEnumerable<NotificationDetail> GetNotificationDetail(long notificationId);        /// <summary>
        /// Update notification value: notification read.
        /// </summary>
        /// <param name="Id">Id of Notification</param>
        /// <returns>
        /// If get notification fit Id, update isRead to true and save all changes success, return true.
        ///     Otherwise, return false.
        /// </returns>
        bool ReadNotification(long Id);/// <summary>
        /// Get count total notifications for account fit param isRead passed
        /// </summary>
        /// <param name="accountId">Id of account</param>
        /// <param name="isRead">Read notification yet or not</param>
        /// <returns>Number of Notification count</returns>
        int CountNotificationForAccount(long accountId, bool isRead = false);
    }
}
