using Microsoft.Extensions.DependencyInjection;
using Aplicatie_Transporturi.Interfaces;
using Aplicatie_Transporturi.Repositories;
using Aplicatie_Transporturi.Services;
namespace Aplicatie_Transporturi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}