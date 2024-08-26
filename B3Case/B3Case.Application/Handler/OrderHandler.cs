using B3Case.Application.Services.TaskServices.Interface;
using B3Case.Core.Schema;
using B3Case.Core.Schema.TaskSchema.Request;
using MediatR;

namespace B3Case.Application.Handler
{
    public class OrderHandler : IRequestHandler<OrderRequest, Result>
    {
        private readonly IManageOrderService _manageOrderService;

        public OrderHandler(IManageOrderService manageOrderService)
        {
            _manageOrderService = manageOrderService;
        }

        public async Task<Result> Handle(OrderRequest request, CancellationToken cancellationToken) =>
                            await _manageOrderService.CreateTask(request);
    }
}
