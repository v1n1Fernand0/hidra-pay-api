using HidraPay.Application.Constants;
using HidraPay.Application.Interfaces;
using HidraPay.Domain.Enums;
using HidraPay.Domain.Ports;

namespace HidraPay.Application.Services
{
    /// <summary>
    /// Seleciona a implementação de IPaymentGateway com base no método.
    /// </summary>
    public class PaymentGatewayFactory
    {
        private readonly IEnumerable<IPaymentGateway> _gateways;

        public PaymentGatewayFactory(IEnumerable<IPaymentGateway> gateways)
        {
            _gateways = gateways;
        }

        public IPaymentGateway GetGateway(PaymentMethod method)
        {
            var gateway = _gateways
                .OfType<IFeaturesMethod>()
                .FirstOrDefault(g => g.SupportedMethod == method);


            if (gateway is null)
                throw new NotSupportedException(
                    string.Format(ErrorMessages.MethodNotSupported, method)
                );

            return (IPaymentGateway)gateway;
        }
    }
}

