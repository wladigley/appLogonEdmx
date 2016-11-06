using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LogonWEB.Startup))]
namespace LogonWEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
