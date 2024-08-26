using B3Case.Core.Schema;
using B3Case.Core.Schema.OrderSchema.Request;
using B3Case.Core.Schema.OrderSchema.Response;
using B3Case.Core.Schema.TaskSchema.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace B3Case.API.Controllers
{
    [ApiController]
    [Route("api/order/[controller]")]
    [ApiExplorerSettings(GroupName = "order")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;

        public OrderController(ILogger<OrderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet()]
        public Task<Result<OrderResponse>> Get([FromQuery] GetOrderRequest request) =>
                _mediator.Send(request);

        [HttpGet("List")]
        public Task<PaginatedResult<OrderResponse>> GetList() =>
                _mediator.Send(new ListOrderRequest());

        [HttpPost()]
        public Task<Result> Post(OrderRequest request) =>
                _mediator.Send(request);
    }
}