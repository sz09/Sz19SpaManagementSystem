using System;
using Microsoft.AspNet.SignalR;
using log4net;
using Infrastructure.Logging;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNet.SignalR.Hubs;

namespace SMGS.Presentation.SignalRChatNotification
{
    [HubName("notificationHub")]
    public class NotificationHub : Hub
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(NotificationHub));

        public bool SendNotifcationForAnUser(long userId, string message, long notificationId)
        {
            logger.EnterMethod();
            try
            {
                Clients.Client(Context.ConnectionId).broadcastMessage(userId, message, notificationId);
                Thread.Sleep(500);
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

        public bool SendNotifcationForAllUsers(string message, long notificationId)
        {
            logger.EnterMethod();
            try
            {
                Clients.All.broadcastMessage(message, notificationId);
                Thread.Sleep(500);
                return true;
            }
            catch (Exception e)
            {
                logger.Error("Erorr: [" + e.Message + "]");
                return false;
            }
            finally
            {
                logger.LeaveMethod();
            }
        }

        public bool SendNotificationForListUsers(List<string> listUsers, string message, long notifcatonId)
        {
            logger.EnterMethod();
            try
            {
                Clients.Users(listUsers).broadcastMessage(message, notifcatonId);
                Thread.Sleep(500);
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

    }
}