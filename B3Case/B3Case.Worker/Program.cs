using B3Case.Infrastructure;
using B3Case.Worker.Workers;

public static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;

                services.AddWorkerApplication(configuration);

                services.AddHostedService<BusWorker>();
            });
}
