using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Permission.Startup))]
namespace Permission
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
