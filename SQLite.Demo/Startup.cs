using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SQLite.Demo.Startup))]
namespace SQLite.Demo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
