using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuggestionApi.Domain.Helpers;
using SuggestionApi.Domain.Helpers.Seed;
using SuggestionApi.Domain.Models;

namespace SuggestionApi.Web.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            using (var scope = host.Services.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                var seed = scope.ServiceProvider.GetRequiredService<ISeedDomainService>();

                try
                {
                    seed.SeedPrefixTree();
                    host.Run();
                }
                catch (Exception exception)
                {
                    logger.LogError(
                        new EventId(0, "Initialization error"),
                        exception,
                        "an error occured while initializing the app");

                    throw;
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
