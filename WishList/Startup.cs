using Microsoft.Owin;
using Owin;
using WishList;

[assembly: OwinStartup(typeof(Startup))]
namespace WishList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
