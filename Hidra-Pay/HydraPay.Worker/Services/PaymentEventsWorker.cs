using HidraPay.Domain.Events;
using HidraPay.Messaging.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HidraPay.Worker.Services
{
    public class PaymentEventsWorker : BackgroundService
    {
        private readonly IEventSubscriber _subscriber;
        private readonly ILogger<PaymentEventsWorker> _logger;

        public PaymentEventsWorker(
            IEventSubscriber subscriber,
            ILogger<PaymentEventsWorker> logger)
        {
            _subscriber = subscriber;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriber.SubscribeAsync<PaymentAuthorizedEvent>(HandleAuthorized);
            _subscriber.SubscribeAsync<PaymentCapturedEvent>(HandleCaptured);
            _subscriber.SubscribeAsync<PaymentRefundedEvent>(HandleRefunded);

            return Task.CompletedTask;
        }

        private Task HandleAuthorized(PaymentAuthorizedEvent @event)
        {
            _logger.LogInformation(
                "Pagamento autorizado: TxId={Tx} Order={Order} Amount={Amt}",
                @event.TransactionId,
                @event.OrderId,
                @event.Amount);

            return Task.CompletedTask;
        }

        private Task HandleCaptured(PaymentCapturedEvent @event)
        {
            _logger.LogInformation(
                "Pagamento capturado: TxId={Tx} Amount={Amt}",
                @event.TransactionId,
                @event.Amount);

            return Task.CompletedTask;
        }

        private Task HandleRefunded(PaymentRefundedEvent @event)
        {
            _logger.LogInformation(
                "Pagamento estornado: TxId={Tx} Amount={Amt}",
                @event.TransactionId,
                @event.Amount);

            return Task.CompletedTask;
        }
    }
}
