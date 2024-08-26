using MediatR;
using System.Text.Json.Serialization;

namespace B3Case.Core.Schema.TaskSchema.Request
{
    public class OrderRequest : IRequest<Result>
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [JsonIgnore]
        public string? Status { get; set; }
    }
}
