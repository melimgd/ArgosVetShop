using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArgosVetShop.Startup))]
namespace ArgosVetShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}