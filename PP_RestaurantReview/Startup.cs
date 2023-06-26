using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PP_RestaurantReview.Startup))]
namespace PP_RestaurantReview
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
