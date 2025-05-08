using HidraPay.Application.Services;
using HidraPay.Domain.ValueObjects;

namespace HidraPay.Application.UseCases.CapturePayment
{
    public class CapturePaymentUseCase : ICapturePaymentUseCase
    {
        private readonly PaymentGatewayFactory _factory;

        public CapturePaymentUseCase(PaymentGatewayFactory factory)
        {
            _factory = factory;
        }

        public Task<PaymentResult> ExecuteAsync(string transactionId, decimal amount, Domain.Enums.PaymentMethod method)
        {
            var gateway = _factory.GetGateway(method);
            return gateway.CaptureAsync(transactionId, amount);
        }
    }
}
