using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PaddleSport.Startup))]
namespace PaddleSport
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
