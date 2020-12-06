using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebAPIOwinAuth.App_Start.OWINAuthStarup))]

namespace WebAPIOwinAuth.App_Start
{
    public class OWINAuthStarup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
