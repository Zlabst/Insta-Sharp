using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InstaSharp.Startup))]
namespace InstaSharp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
