using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BusMeApp.Startup))]
namespace BusMeApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);           
        }
    }
}
