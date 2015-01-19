using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(scrapping.Startup))]
namespace scrapping
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
