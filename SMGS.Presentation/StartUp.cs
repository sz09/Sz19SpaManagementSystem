using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SMGS.Presentation.SignalRChatNotification.StartUp))]

namespace SMGS.Presentation.SignalRChatNotification
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
