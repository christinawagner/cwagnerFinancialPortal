using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cwagnerFinancialPortal.Startup))]
namespace cwagnerFinancialPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
