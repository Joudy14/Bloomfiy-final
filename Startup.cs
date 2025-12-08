using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bloomfiy_final.Startup))]
namespace Bloomfiy_final
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
