using HidraPay.Domain.Enums;

namespace HidraPay.Domain.ValueObjects
{
    /// <summary>
    /// Resultado padronizado de uma operação de pagamento.
    /// </summary>
    public record PaymentResult(
        string TransactionId,
        PaymentStatus Status,
        decimal Amount
    );
}
