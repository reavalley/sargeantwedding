using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BensWedding.Startup))]
namespace BensWedding
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
