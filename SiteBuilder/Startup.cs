using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SiteBuilder.Startup))]
namespace SiteBuilder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
