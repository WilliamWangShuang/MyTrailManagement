using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyProfileTrail.Startup))]
namespace MyProfileTrail
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
