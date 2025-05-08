using HidraPay.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HidraPay.Application.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<PaymentGatewayFactory>();
            services.AddTransient<PaymentService>();

            return services;
        }
    }
}
