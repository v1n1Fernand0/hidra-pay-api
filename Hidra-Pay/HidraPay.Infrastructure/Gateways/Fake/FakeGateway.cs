using HidraPay.Domain.Enums;
using HidraPay.Domain.Ports;
using HidraPay.Domain.ValueObjects;

namespace HidraPay.Infrastructure.Gateways
{
    /// <summary>
    /// Gateway “fake” para testes: sempre autoriza/captura/refunda imediatamente.
    /// </summary>
    public class FakeGateway : IPaymentGateway
    {
        public Task<PaymentResult> AuthorizeAsync(PaymentRequest request)
        {
            return Task.FromResult(new PaymentResult(
                TransactionId: $"fake-{request.OrderId}",
                Status: PaymentStatus.Authorized,
                Amount: request.Amount
            ));
        }

        public Task<PaymentResult> CaptureAsync(string transactionId, decimal amount)
        {
            return Task.FromResult(new PaymentResult(
                transactionId,
                Status: PaymentStatus.Captured,
                Amount: amount
            ));
        }

        public Task<PaymentResult> RefundAsync(string transactionId, decimal amount)
        {
            return Task.FromResult(new PaymentResult(
                transactionId,
                Status: PaymentStatus.Refunded,
                Amount: amount
            ));
        }

        public Task<PaymentStatus> GetStatusAsync(string transactionId)
        {
            return Task.FromResult(PaymentStatus.Authorized);
        }
    }
}
