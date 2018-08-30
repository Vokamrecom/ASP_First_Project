using System.Diagnostics;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;

using Newtonsoft.Json;
using Owin;
using WebApplication3.App_Start;

[assembly: OwinStartupAttribute(typeof(WebApplication3.Startup))]
namespace WebApplication3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
                var settings = new JsonSerializerSettings { ContractResolver = new SignalRContractResolver() };
                var serializer = JsonSerializer.Create(settings);
                GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => serializer);

                app.Map("/signalr", map =>
                {
                    // map.UseCors(CorsOptions.AllowAll);
                    map.RunSignalR();
                   // Process.Start(@"C:\Users\Vokamrecom-PC\Desktop\ASPDotNet_Auth_Login-master\ASPDotNet_Auth_Login-master\Client\src\index.html");
                 

                });
            
        }

    }
}
