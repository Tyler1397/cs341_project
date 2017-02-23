using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cs341_project.Startup))]
namespace cs341_project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
