using B3Case.Core.Schema.TaskSchema.Request;
using B3Case.Core.Schema;
using B3Case.Core.Schema.OrderSchema.Request;

namespace B3Case.Application.Services.TaskServices.Interface
{
    public interface IManageOrderService
    {
        Task<Result> CreateTask(OrderRequest request);
        Task<Result> SaveOrder(WorkerOrderRequest request);
    }
}