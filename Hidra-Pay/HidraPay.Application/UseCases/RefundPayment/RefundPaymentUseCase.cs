using HidraPay.Application.Constants;
using HidraPay.Application.Services;
using HidraPay.Domain.Enums;
using HidraPay.Domain.Events;
using HidraPay.Domain.Ports;
using HidraPay.Domain.ValueObjects;
using HidraPay.Messaging.Interfaces;

namespace HidraPay.Application.UseCases.RefundPayment
{
    public class RefundPaymentUseCase : IRefundPaymentUseCase
    {
        private readonly PaymentGatewayFactory _factory;
        private readonly IPaymentRepository _repo;
        private readonly IEventPublisher _events;

        public RefundPaymentUseCase(
            PaymentGatewayFactory factory,
            IPaymentRepository repo,
            IEventPublisher events)
        {
            _factory = factory;
            _repo = repo;
            _events = events;
        }

        public async Task<PaymentResult> ExecuteAsync(string txId, decimal amount, PaymentMethod method)
        {
            var transaction = await _repo.GetByTransactionIdAsync(txId)
                ?? throw new InvalidOperationException(
                    string.Format(ErrorMessages.TransactionNotFound, txId));

            var result = await _factory
                .GetGateway(method)
                .RefundAsync(txId, amount);

            transaction.UpdateStatus(result.Status);
            await _repo.UpdateAsync(transaction);

            await _events.PublishAsync(new PaymentRefundedEvent(
                transactionId: txId,
                amount: result.Amount
            ));

            return result;
        }
    }
}
