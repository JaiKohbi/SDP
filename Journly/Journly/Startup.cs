using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Journly.Startup))]
namespace Journly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
