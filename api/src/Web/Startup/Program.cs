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
using SuggestionApi.Appplication.Seed;
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
                const string geoFileName = "cities_canada-usa.tsv";
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                var seed = scope.ServiceProvider.GetRequiredService<ISeedDomainService>();

                try
                {
                    Console.WriteLine($" ************ dotnet.exe process id: {Process.GetCurrentProcess().Id} ************");
                    using (var reader = new StreamReader(@$"..\..\api\src\Domain\DataSource\{geoFileName}"))
                    {
                        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                        csv.Configuration.Delimiter = "\t";
                        csv.Configuration.HasHeaderRecord = true;
                        
                        csv.Configuration.BadDataFound = x =>
                        {
                            Console.WriteLine($"Bad data: <{x.RawRecord}>");
                        };
                        
                        var geoNames = csv.GetRecords<GeoNameInput>();
                        System.Threading.Thread.Sleep(10000);
                        seed.SeedPrefixTree(geoNames);
                    }

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
