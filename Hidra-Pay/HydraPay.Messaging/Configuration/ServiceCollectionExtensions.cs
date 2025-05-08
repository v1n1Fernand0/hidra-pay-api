using HidraPay.Messaging.Interfaces;
using HidraPay.Messaging.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace HidraPay.Messaging.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessaging(
            this IServiceCollection services, IConfiguration config)
        {
            var rabbitMqSettings = new RabbitMqSettings();
            config.GetSection("RabbitMq").Bind(rabbitMqSettings); 

            services.AddSingleton(rabbitMqSettings);

            services.AddSingleton<IEventPublisher, RabbitMqPublisher>();
            services.AddSingleton<IEventSubscriber, RabbitMqSubscriber>();

            return services;
        }
    }
}
