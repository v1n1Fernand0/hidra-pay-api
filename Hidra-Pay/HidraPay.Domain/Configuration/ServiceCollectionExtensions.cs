using Microsoft.Extensions.DependencyInjection;

namespace HidraPay.Domain.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services;
        }
    }
}
