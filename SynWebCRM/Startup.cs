using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SynWebCRM.Startup))]
namespace SynWebCRM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
