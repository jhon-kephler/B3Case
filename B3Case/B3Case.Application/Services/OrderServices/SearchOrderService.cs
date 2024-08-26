using AutoMapper;
using B3Case.Application.Services.OrderServices.Interface;
using B3Case.Core.Schema;
using B3Case.Core.Schema.OrderSchema.Request;
using B3Case.Core.Schema.OrderSchema.Response;
using B3Case.Domain.Entities;
using B3Case.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace B3Case.Application.Services.OrderServices
{
    public class SearchOrderService : ISearchOrderService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Order> _repository;
        private readonly ILogger<SearchOrderService> _logger;

        public SearchOrderService(IMapper mapper, IRepository<Order> repository, ILogger<SearchOrderService> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result<OrderResponse>> GetOrder(GetOrderRequest request)
        {
            var result = new Result<OrderResponse>();

            try
            {
                _logger.LogInformation("Fetching Order with ID: {OrderId} at {time}", request.Id, DateTimeOffset.Now);

                var order = _repository.GetById(request.Id);
                if (order == null)
                {
                    result.SetError("Invalid Id");
                    _logger.LogWarning("Order with ID: {OrderId} not found at {time}", request.Id, DateTimeOffset.Now);
                    return result;
                }

                result.SetSuccess(_mapper.Map<OrderResponse>(order));
                _logger.LogInformation("Order with ID: {OrderId} fetched successfully at {time}", request.Id, DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message);
                _logger.LogError(ex, "Error occurred while fetching Order with ID: {OrderId} at {time}", request.Id, DateTimeOffset.Now);
            }

            return result;
        }

        public async Task<PaginatedResult<OrderResponse>> ListOrder()
        {
            var result = new PaginatedResult<OrderResponse>();

            try
            {
                _logger.LogInformation("Listing all orders at {time}", DateTimeOffset.Now);

                var listOrder = _repository.GetAll();
                result = new PaginatedResult<OrderResponse>(_mapper.Map<List<OrderResponse>>(listOrder));

                _logger.LogInformation("Successfully listed all orders at {time}", DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message);
                _logger.LogError(ex, "Error occurred while listing orders at {time}", DateTimeOffset.Now);
            }

            return result;
        }
    }
}
