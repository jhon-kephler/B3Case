using B3Case.Data;
using B3Case.Data.Repositories;
using B3Case.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using B3Case.Application.Services.TaskServices;
using Microsoft.Extensions.DependencyInjection;
using B3Case.Application.Services.OrderServices;
using B3Case.Application.Services.RabbitServices;
using B3Case.Application.Services.OrderServices.Interface;
using B3Case.Application.Services.RabbitServices.Interface;
using B3Case.Application.Services.TaskServices.Interface;
using B3Case.Application.Services.WorkerServices.Interfaces;
using B3Case.Application.Services.WorkerServices;

namespace B3Case.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext(configuration);
            services.AddServices();
            services.AddHandler();
            services.AddAutoMapper();
            services.AddRepository();
            services.AddBus();

            return services;
        }

        public static IServiceCollection AddWorkerApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext(configuration);
            services.AddWorkerServices();
            services.AddAutoMapper();
            services.AddRepository();
            services.AddBus();

            return services;
        }

        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<B3CaseContext>(options => { options.UseNpgsql(configuration["ConnectionString:DefaultConnection"]); });

        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped(typeof(DbContext), typeof(B3CaseContext));

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(Core.AssemblyReference.Assembly);

        public static IServiceCollection AddHandler(this IServiceCollection services)
        {
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));

            return services;
        }

        public static IServiceCollection AddBus(this IServiceCollection services)
        {
            services.AddScoped<IBusService, BusService>();
            return services;
        }

        public static IServiceCollection AddWorkerServices(this IServiceCollection services)
        {
            services.AddScoped<IManageOrderService, ManageOrderService>();
            services.AddScoped<IManageWorkerOrderService, ManageWorkerOrderService>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IManageOrderService, ManageOrderService>();
            services.AddScoped<ISearchOrderService, SearchOrderService>();

            return services;
        }
    }
}