using B3Case.Application.Services.OrderServices.Interface;
using B3Case.Core.Schema.OrderSchema.Request;
using B3Case.Core.Schema.OrderSchema.Response;
using B3Case.Core.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3Case.Application.Handler
{
    public class GetOrderHandler : IRequestHandler<GetOrderRequest, Result<OrderResponse>>
    {
        private readonly ISearchOrderService _searchOrderService;

        public GetOrderHandler(ISearchOrderService searchOrderService)
        {
            _searchOrderService = searchOrderService;
        }

        public async Task<Result<OrderResponse>> Handle(GetOrderRequest request, CancellationToken cancellationToken) =>
                            await _searchOrderService.GetOrder(request);
    }
}
