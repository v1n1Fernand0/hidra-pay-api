using HidraPay.Messaging.Configuration;
using HidraPay.Messaging.Interfaces;
using Microsoft.Extensions.Options;

namespace HidraPay.Messaging.Services
{
    public class RabbitMqSubscriber : IEventSubscriber
    {
        private readonly RabbitMqSettings _settings;
        public RabbitMqSubscriber(IOptions<RabbitMqSettings> opts)
        {
            _settings = opts.Value;
            // TODO: inicializar conexão / channel de consumo
        }

        public Task SubscribeAsync<TEvent>(Func<TEvent, Task> handler, CancellationToken cancellationToken = default)
        {
            // TODO: inscrever no RabbitMQ e chamar handler quando chegar mensagem
            return Task.CompletedTask;
        }
    }
}
