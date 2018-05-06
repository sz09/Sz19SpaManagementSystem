using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMGS.Presentation.SignalRChatNotification.GetUser
{
    public class CustomUserProvider : IUserIdProvider
    {
        public string GetUserId(IRequest iRequest)
        {
            // user_id = di_resu
            return iRequest.Cookies["di_resu"].Value;
        }
    }
}