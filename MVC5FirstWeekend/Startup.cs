using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC5FirstWeekend.Startup))]
namespace MVC5FirstWeekend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
