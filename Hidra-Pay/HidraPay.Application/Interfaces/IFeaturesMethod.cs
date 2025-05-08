using HidraPay.Domain.Enums;

namespace HidraPay.Application.Interfaces
{
    /// <summary>
    /// Indica quais métodos de pagamento o gateway suporta.
    /// </summary>
    public interface IFeaturesMethod
    {
        IReadOnlyCollection<PaymentMethod> SupportedMethods { get; }
    }
}
