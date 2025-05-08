using HidraPay.Application.Services;
using HidraPay.Domain.ValueObjects;

namespace HidraPay.Application.UseCases.RefundPayment
{
    public class RefundPaymentUseCase : IRefundPaymentUseCase
    {
        private readonly PaymentGatewayFactory _factory;

        public RefundPaymentUseCase(PaymentGatewayFactory factory)
        {
            _factory = factory;
        }

        public Task<PaymentResult> ExecuteAsync(string transactionId, decimal amount, HidraPay.Domain.Enums.PaymentMethod method)
        {
            var gateway = _factory.GetGateway(method);
            return gateway.RefundAsync(transactionId, amount);
        }
    }
}
