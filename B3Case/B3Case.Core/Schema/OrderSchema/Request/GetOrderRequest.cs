using B3Case.Core.Schema.OrderSchema.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3Case.Core.Schema.OrderSchema.Request
{
    public class GetOrderRequest : IRequest<Result<OrderResponse>>
    {
        public int Id { get; set; }
    }
}
