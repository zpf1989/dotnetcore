using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace hw_mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel() //指定web服务
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()// hosting in IIS and IIS Express
                .UseStartup<Startup>()
                //When true, the host will capture any exceptions from the Startup class and attempt to start the server
                .CaptureStartupErrors(true)
                //When true (or when Environment is set to "Development"), 
                //the app will display details of startup exceptions, instead of just a generic error page
                // .UseSetting("detailedErrors","true")
                .Build();

            host.Run();
        }
    }
}
