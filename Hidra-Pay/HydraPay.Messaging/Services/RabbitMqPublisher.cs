using System.Threading.Tasks;
using HidraPay.Messaging.Configuration;
using HidraPay.Messaging.Interfaces;
using Microsoft.Extensions.Options;

namespace HidraPay.Messaging.Services
{
    public class RabbitMqPublisher : IEventPublisher
    {
        private readonly RabbitMqSettings _settings;
        public RabbitMqPublisher(IOptions<RabbitMqSettings> opts)
        {
            _settings = opts.Value;
            // TODO: inicializar conexão com RabbitMQ
        }

        public Task PublishAsync<TEvent>(TEvent @event)
        {
            // TODO: serializar e publicar no RabbitMQ
            return Task.CompletedTask;
        }
    }
}
