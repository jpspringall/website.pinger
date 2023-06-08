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

                .Build();
            host.Run();
        }
    }
}