using HidraPay.Application.Services;
using HidraPay.Domain.Entities;
using HidraPay.Domain.Events;
using HidraPay.Domain.Ports;
using HidraPay.Domain.ValueObjects;
using HidraPay.Messaging.Interfaces;

namespace HidraPay.Application.UseCases.AuthorizePayment
{
    public class AuthorizePaymentUseCase : IAuthorizePaymentUseCase
    {
        private readonly PaymentGatewayFactory _factory;
        private readonly IPaymentRepository _repo;
        private readonly IEventPublisher _events;

        public AuthorizePaymentUseCase(
            PaymentGatewayFactory factory,
            IPaymentRepository repo,
            IEventPublisher events)
        {
            _factory = factory;
            _repo = repo;
            _events = events;
        }

        public async Task<PaymentResult> ExecuteAsync(PaymentRequest request)
        {
            var result = await _factory
                .GetGateway(request.Method)
                .AuthorizeAsync(request);

            var transaction = new PaymentTransaction(
                orderId: request.OrderId,
                transactionId: result.TransactionId,
                amount: result.Amount,
                currency: request.Currency,
                method: request.Method,
                status: result.Status
            );
            await _repo.AddAsync(transaction);

            await _events.PublishAsync(new PaymentAuthorizedEvent(
                transactionId: result.TransactionId,
                orderId: request.OrderId,
                amount: result.Amount
            ));

            return result;
        }
    }
}
