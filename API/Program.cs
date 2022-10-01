using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // using, causes garbage collection to clear scope var from memory after it is Main function runs.
            using var scope = host.Services.CreateScope(); // will host all our services

            var services = scope.ServiceProvider; // allow access to services
            try
            {
                // Service locator pattern, allows us to grab the service we setup for the DataContext
                var context = services.GetRequiredService<DataContext>(); // service of type DataContext
                await context.Database.MigrateAsync(); // applies any pending migrations for the context and create db if none available
                await Seed.SeedData(context);
            }
            catch (System.Exception ex)
            {
                // Log any errors that could come up
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error ocurred during migration");
            }

            await host.RunAsync(); // now we run the server
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
