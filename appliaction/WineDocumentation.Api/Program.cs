using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace WineDocumentation.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
           // CreateHostBuilder(args).Build().Run();
           var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

    //     public static IHostBuilder CreateHostBuilder(string[] args) =>
    //         Host.CreateDefaultBuilder(args)
    //             .ConfigureWebHostDefaults(webBuilder =>
    //             {
    //                 webBuilder.UseStartup<Startup>();
    //             });
    
    }
}
