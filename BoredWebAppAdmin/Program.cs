using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoredWebAppAdmin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((context, configBuilder) =>
                    {
                        string envName = context.HostingEnvironment.EnvironmentName;
                        configBuilder.Sources.Clear();
                        configBuilder.AddJsonFile("admin.appsettings.json", optional: false);
                    });
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://*:80");
                });
    }
}
