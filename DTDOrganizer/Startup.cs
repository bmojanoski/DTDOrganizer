using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DTDOrganizer.Startup))]
namespace DTDOrganizer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
