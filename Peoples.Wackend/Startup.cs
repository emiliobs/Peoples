using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Peoples.Wackend.Startup))]
namespace Peoples.Wackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
