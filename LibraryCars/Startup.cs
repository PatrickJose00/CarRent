using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibraryCars.Startup))]
namespace LibraryCars
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
