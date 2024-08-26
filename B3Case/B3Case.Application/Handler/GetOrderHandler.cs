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
    public class ListOrderHandler : IRequestHandler<ListOrderRequest, PaginatedResult<OrderResponse>>
    {
        private readonly ISearchOrderService _searchOrderService;

        public ListOrderHandler(ISearchOrderService searchOrderService)
        {
            _searchOrderService = searchOrderService;
        }

        public async Task<PaginatedResult<OrderResponse>> Handle(ListOrderRequest request, CancellationToken cancellationToken) =>
                            await _searchOrderService.ListOrder();
    }
}
