using SPMS.ObjectModel.Entities;
using System.Collections.Generic;

namespace Core.ObjectServices.Repositories
{
    public interface INotificationRepository
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
        /// Get collection of notification fit accountId but not read yet
        /// </summary>
        /// <param name="accountId">Id of account</param>
        /// <param name="isRead">Read notification yet or not</param>
        /// <returns>
        /// Collection of Notification if found.
        ///     Otherwise, return null
        /// </returns>
        IEnumerable<Notification> GetNotificationForAccount(long accountId, bool isRead = false);
        /// <summary>
        /// Get detail of notification
        ///     Collection if 
        /// </summary>
        /// <param name="notificationId">Id of notification</param>
        /// <returns>
        /// Collection of notification detail if found.
        ///     Otherwise, return null.
        /// </returns>
        IEnumerable<NotificationDetail> GetNotificationDetail(long notificationId);
        /// <summary>
        /// Get count total notifications for account fit param isRead passed
        /// </summary>
        /// <param name="accountId">Id of account</param>
        /// <param name="isRead">Read notification yet or not</param>
        /// <returns>Number of Notification count</returns>
        int CountNotificationForAccount(long accountId, bool isRead = false);
        /// <summary>
        /// Inserted new Notification and save all changes
        /// </summary>
        /// <param name="notification">Notification type</param>
        /// <returns>
        /// If inserted Notification to database and save all changes success, return true.
        ///     Otherwise, return false.
        /// </returns>
        bool CreateNewNotification(Notification notification);
        /// <summary>
        /// Inserted new NotificationDetail and save all changes
        /// </summary>
        /// <param name="notificationDetail">Notification type</param>
        /// <returns>
        /// If inserted NotificationDetail to database and save all changes success, return true.
        ///     Otherwise, return false.
        /// </returns>
        bool CreateNewNotificationDetail(NotificationDetail notificationDetail);
        /// <summary>
        /// Update notification values
        /// </summary>
        /// <param name="notification">Notification type</param>
        /// <returns>
        /// If update notification and save all changes success, return true.
        ///     Otherwise, return false
        /// </returns>
        bool UpdateNotification(Notification notification);
        /// <summary>
        /// Update NotificationDetail and save all changes
        /// </summary>
        /// <param name="notificationDetail">NotificationDetail type</param>
        /// <returns>
        /// Update NotificationDetail and save all changes success, return true.
        ///     Othewise, return false.
        /// </returns>
        bool UpdateNotificationDetail(NotificationDetail notificationDetail);
        /// <summary>
        /// Update notification value: notification read.
        /// </summary>
        /// <param name="Id">Id of Notification</param>
        /// <returns>
        /// If get notification fit Id, update isRead to true and save all changes success, return true.
        ///     Otherwise, return false.
        /// </returns>
        bool ReadNotification(long Id);

    }
}
