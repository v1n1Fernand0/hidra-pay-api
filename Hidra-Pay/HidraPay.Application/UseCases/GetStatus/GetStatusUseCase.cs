using HidraPay.Application.Services;
using HidraPay.Domain.Enums;

namespace HidraPay.Application.UseCases.GetStatus
{
    public class GetStatusUseCase : IGetStatusUseCase
    {
        private readonly PaymentGatewayFactory _factory;

        public GetStatusUseCase(PaymentGatewayFactory factory)
            => _factory = factory;

        public Task<PaymentStatus> ExecuteAsync(string transactionId, PaymentMethod method)
        {
            var gateway = _factory.GetGateway(method);
            return gateway.GetStatusAsync(transactionId);
        }
    }
}
