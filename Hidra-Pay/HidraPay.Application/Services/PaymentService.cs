using HidraPay.Domain.Enums;
using HidraPay.Domain.ValueObjects;

namespace HidraPay.Application.Services
{
    /// <summary>
    /// Orquestra o fluxo de pagamento usando a fábrica de gateways.
    /// </summary>
    public class PaymentService
    {
        private readonly PaymentGatewayFactory _factory;

        public PaymentService(PaymentGatewayFactory factory)
        {
            _factory = factory;
        }

        public Task<PaymentResult> AuthorizeAsync(PaymentRequest request)
        {
            var gateway = _factory.GetGateway(request.Method);
            return gateway.AuthorizeAsync(request);
        }

        public Task<PaymentResult> CaptureAsync(string transactionId, decimal amount, PaymentMethod method)
        {
            var gateway = _factory.GetGateway(method);
            return gateway.CaptureAsync(transactionId, amount);
        }

        public Task<PaymentResult> RefundAsync(string transactionId, decimal amount, PaymentMethod method)
        {
            var gateway = _factory.GetGateway(method);
            return gateway.RefundAsync(transactionId, amount);
        }

        public Task<PaymentStatus> GetStatusAsync(string transactionId, PaymentMethod method)
        {
            var gateway = _factory.GetGateway(method);
            return gateway.GetStatusAsync(transactionId);
        }
    }
}
