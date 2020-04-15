using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RTA_Project_MVC.Startup))]
namespace RTA_Project_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
