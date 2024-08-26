using AutoMapper;
using B3Case.Application.Services.RabbitServices.Interface;
using B3Case.Application.Services.TaskServices.Interface;
using B3Case.Core.Schema;
using B3Case.Core.Schema.OrderSchema.Request;
using B3Case.Core.Schema.TaskSchema.Request;
using B3Case.Domain.Entities;
using B3Case.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace B3Case.Application.Services.TaskServices
{
    public class ManageOrderService : IManageOrderService
    {
        private readonly IMapper _mapper;
        private readonly IBusService _busService;
        private readonly IRepository<Order> _repository;
        private readonly ILogger<ManageOrderService> _logger;

        public ManageOrderService(IMapper mapper, IBusService busService, IRepository<Order> repository, ILogger<ManageOrderService> logger)
        {
            _mapper = mapper;
            _busService = busService;
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result> CreateTask(OrderRequest request)
        {
            var result = new Result();
            request.Status = "process";

            try
            {
                var order = await _repository.AddAsync(_mapper.Map<Order>(request));
                _logger.LogInformation("Order created successfully with ID: {OrderId} at {time}", order.Id, DateTimeOffset.Now);

                var message = JsonSerializer.Serialize(_mapper.Map<WorkerOrderRequest>(order));
                _busService.SendMessage(message, "order_queue");
                _logger.LogInformation("Message sent to 'order_queue' for Order ID: {OrderId} at {time}", order.Id, DateTimeOffset.Now);

                result.SetSuccess();
                _logger.LogInformation("Task completed successfully for Order ID: {OrderId} at {time}", order.Id, DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message);
                _logger.LogError(ex, "Error occurred while creating task at {time}", DateTimeOffset.Now);
            }

            return result;
        }

        public async Task<Result> SaveOrder(WorkerOrderRequest request)
        {
            var result = new Result();

            try
            {
                _logger.LogInformation("Starting to save Order ID: {OrderId} with status 'processed' at {time}", request.Id, DateTimeOffset.Now);

                request.Status = "processed";
                _repository.Update(request.Id, _mapper.Map<Order>(request));

                result.SetSuccess();
                _logger.LogInformation("Order ID: {OrderId} saved successfully at {time}", request.Id, DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message);
                _logger.LogError(ex, "Error occurred while saving Order ID: {OrderId} at {time}", request.Id, DateTimeOffset.Now);
            }

            return result;
        }
    }
}
