namespace AuthenticationService
{
    class Program
    {
        static void Main(string[] args)
        {
            StartApp();
            CreateHostBuilder(args).Build().Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { 
                webBuilder.UseStartup<Startup>();
            });

        static void StartApp() => Logger.CreateLogDirectory();
    }
}

