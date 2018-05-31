using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GIPHYPop.Startup))]
namespace GIPHYPop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
