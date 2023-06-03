using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Lative.Application;
using Lative.DataAccess;
using Lative.Infrastructure;

namespace Lative.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                
                try
                {
                    var csvProcessor = services.GetRequiredService<ICsvProcessor>();
                    var dataImporter = services.GetRequiredService<IDataImporter>();
                    var tableInitializer = services.GetRequiredService<ITableInitializer>();

                    var config = services.GetRequiredService<IConfiguration>();
                    var filePath = config.GetValue<string>("CsvProcessorConfig:FilePath");
                    var bulkSize = config.GetValue<int>("CsvProcessorConfig:BulkSize");

                    tableInitializer.InitializeTables();
                    var saleOperationModels = csvProcessor.ReadCsv(filePath);

                    dataImporter.BulkInsertSalesAndDimensions(saleOperationModels, bulkSize);
                    
                    Console.WriteLine("Data import completed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred during data import: " + ex.Message);
                }
            }
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.SetBasePath(AppContext.BaseDirectory);
                    config.AddJsonFile("appsettings.json", optional: false);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;
                    var connectionString = configuration.GetConnectionString("DefaultConnection");

                    services.AddSingleton<ICsvProcessor, CsvProcessor>();
                    services.AddSingleton<IIndexMap, IndexMap>();
                    services.AddSingleton<IDataImporter>(provider => new DataImporter(connectionString));
                    services.AddScoped<ITableInitializer>(provider => new TableInitializer(connectionString));
                });



    }
}
