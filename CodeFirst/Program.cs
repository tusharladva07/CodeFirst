using CodeFirst.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

            
        }

        public static IWebHost BuildWebHost(string[] args) =>
             new WebHostBuilder()
             .UseKestrel()
             .UseContentRoot(Directory.GetCurrentDirectory())
             .UseIISIntegration()
             .UseDefaultServiceProvider(option =>
             option.ValidateScopes = false)
             .UseStartup<Startup>()
             .Build();

        //public static IWebHost BuildWebHost(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>()
        //        .Build();





    }
}
