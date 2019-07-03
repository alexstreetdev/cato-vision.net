
using ImageStore.Services.Database;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ImageStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = CreateWebHostBuilder(args).Build();
            //CreateWebHostBuilder(args).Build().Run();

            var db = (IDbClient)host.Services.GetService(typeof(IDbClient));
            db.InitialiseDatabase();

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json");
                    config.AddCommandLine(args);
                })
                .UseStartup<Startup>()
                .UseUrls(urls: "http://*:5000");
    }
}
