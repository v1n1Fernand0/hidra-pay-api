using HidraPay.Application.Services;
using HidraPay.Domain.ValueObjects;

namespace HidraPay.Application.UseCases.AuthorizePayment
{
    public class AuthorizePaymentUseCase : IAuthorizePaymentUseCase
    {
        private readonly PaymentGatewayFactory _factory;

        public AuthorizePaymentUseCase(PaymentGatewayFactory factory)
        {
            _factory = factory;
        }

        public Task<PaymentResult> ExecuteAsync(PaymentRequest request)
        {
            var gateway = _factory.GetGateway(request.Method);
            return gateway.AuthorizeAsync(request);
        }
    }
}
