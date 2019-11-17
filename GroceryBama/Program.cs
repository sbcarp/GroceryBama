using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace GroceryBama
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IWebHostBuilder webHostBuilder = WebHost.CreateDefaultBuilder(args);
            if (env == Environments.Development) 
            {
                webHostBuilder.UseUrls("http://*:30103", "https://*:30104");
            }
            else
            {
                webHostBuilder.UseKestrel(options =>
                 {
                     options.ListenAnyIP(30104, listenOptions =>
                     {
                         listenOptions.UseHttps("grocerybama.com.pfx", "");
                     });
                 });
            }

            return webHostBuilder.UseStartup<Startup>();
        }
        
    }
}
