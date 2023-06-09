namespace Website.Pinger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(new string[] { "http://*:4000" });
                })
                .ConfigureServices(services =>
                {
                    services.AddHttpClient();
                    services.AddHostedService<Workers.Pinger>();

                })
                .ConfigureAppSettings()
                .Build();

            host.Run();
        }
    }

    public static class WebApplicationBuilder
    {
        public static IHostBuilder ConfigureAppSettings(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration((ctx, builder) =>
            {
                builder.SetBasePath(ctx.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();
            });

            return host;
        }
    }
}