using AutoMapper;
using B3Case.Core.Schema.OrderSchema.Request;
using B3Case.Core.Schema.OrderSchema.Response;
using B3Case.Core.Schema.TaskSchema.Request;
using B3Case.Domain.Entities;

namespace B3Case.Core.Mapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderRequest, Order>()
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description))
                .ForMember(dest => dest.Status, src => src.MapFrom(x => x.Status))
                .ForMember(dest => dest.Date, src => src.MapFrom(x => x.Date));

            CreateMap<WorkerOrderRequest, Order>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description))
                .ForMember(dest => dest.Status, src => src.MapFrom(x => x.Status))
                .ForMember(dest => dest.Date, src => src.MapFrom(x => x.Date));

            CreateMap<Order, WorkerOrderRequest>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description))
                .ForMember(dest => dest.Status, src => src.MapFrom(x => x.Status))
                .ForMember(dest => dest.Date, src => src.MapFrom(x => x.Date));

            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description))
                .ForMember(dest => dest.Status, src => src.MapFrom(x => x.Status))
                .ForMember(dest => dest.Date, src => src.MapFrom(x => x.Date));
        }
    }
}