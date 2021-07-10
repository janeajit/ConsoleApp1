using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldConsoleApp1;

namespace WebApplication1
{
    public class Programs
    {
        public static void Main(string[] args)
        {
            static void Main(string[] args)
            {
                constructorExample c = new constructorExample();
                constructorExample c1 = new constructorExample("ajit");
                Console.WriteLine("c1" + c.Example1);
                Console.WriteLine("c1" + c.ajit);

                Console.WriteLine("c2" + c1.Example2);
            }
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
