using Microsoft.Extensions.DependencyInjection;
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

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperAdmin", policy => policy.RequireRole("SuperAdmin"));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Customer", policy => policy.RequireRole("Customer"));
            });
        }
    }
}
