using B3Case.Core.Schema.OrderSchema.Response;
using B3Case.Core.Schema;
using B3Case.Core.Schema.OrderSchema.Request;

namespace B3Case.Application.Services.OrderServices.Interface
{
    public interface ISearchOrderService
    {
        Task<Result<OrderResponse>> GetOrder(GetOrderRequest request);
        Task<PaginatedResult<OrderResponse>> ListOrder();
    }
}