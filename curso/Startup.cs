using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(curso.Startup))]
namespace curso
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
