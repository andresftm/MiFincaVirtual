using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MiFincaVirtual.Backend.Startup))]
namespace MiFincaVirtual.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
