using B3Case.Application.Services.WorkerServices.Interfaces;

namespace B3Case.Worker.Workers
{
    public class BusWorker : BackgroundService
    {
        private readonly ILogger<BusWorker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private const int DelayMilliseconds = 1000;

        public BusWorker(ILogger<BusWorker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker starting at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var workerOrderService = scope.ServiceProvider.GetRequiredService<IManageWorkerOrderService>();

                        _logger.LogInformation("BusWorker running at: {time}", DateTimeOffset.Now);

                        workerOrderService.Consuming();
                    }

                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                    await Task.Delay(DelayMilliseconds, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while executing the worker.");
                }
            }

            _logger.LogInformation("Worker stopping at: {time}", DateTimeOffset.Now);
        }
    }
}
