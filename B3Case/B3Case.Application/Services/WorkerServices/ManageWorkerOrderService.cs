using B3Case.Application.Services.RabbitServices.Interface;
using B3Case.Application.Services.TaskServices.Interface;
using B3Case.Application.Services.WorkerServices.Interfaces;
using B3Case.Core.Schema.OrderSchema.Request;
using B3Case.Core.Schema.TaskSchema.Request;
using Microsoft.Extensions.Logging;

namespace B3Case.Application.Services.WorkerServices
{
    public class ManageWorkerOrderService : IManageWorkerOrderService
    {
        private readonly IBusService _busService;
        private readonly IManageOrderService _manageOrderService;
        private readonly ILogger<ManageWorkerOrderService> _logger;

        public ManageWorkerOrderService(IBusService busService, IManageOrderService manageOrderService, ILogger<ManageWorkerOrderService> logger)
        {
            _busService = busService;
            _manageOrderService = manageOrderService;
            _logger = logger;
        }

        public void Consuming()
        {
            try
            {
                _logger.LogInformation("Starting to consume message from 'order_queue' at {time}", DateTimeOffset.Now);

                var request = _busService.Consuming<WorkerOrderRequest>("order_queue");
                _logger.LogInformation("Message consumed from 'order_queue' with Order ID: {OrderId} at {time}", request?.Id, DateTimeOffset.Now);

                _manageOrderService.SaveOrder(request);
                _logger.LogInformation("Order with ID: {OrderId} processed and saved successfully at {time}", request?.Id, DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while consuming or processing message from 'order_queue' at {time}", DateTimeOffset.Now);
            }
        }

    }
}