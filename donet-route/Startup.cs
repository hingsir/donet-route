using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(donet_route.Startup))]
namespace donet_route
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
