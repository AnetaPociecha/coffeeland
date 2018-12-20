using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;


[assembly: OwinStartup(typeof(Coffeeland.Startup))]

namespace Coffeeland
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR("/Client/dist", new HubConfiguration());
        }
    }
}
