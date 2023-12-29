namespace telegit;
using DotNetEnv;

public abstract class Program
{
    public static void Main(string[] args)
    {
        Env.Load();
        TelegramClient.TelegramClient.Init(Environment.GetEnvironmentVariable("TELEGRAM_API_TOKEN"), Environment.GetEnvironmentVariable("TELEGRAM_CHAT_ID"));
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}