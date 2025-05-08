using HidraPay.Domain.ValueObjects;

namespace HidraPay.Application.UseCases.RefundPayment
{
    /// <summary>
    /// Caso de uso: reembolsar um pagamento capturado.
    /// </summary>
    public interface IRefundPaymentUseCase
    {
        Task<PaymentResult> ExecuteAsync(string transactionId, decimal amount, Domain.Enums.PaymentMethod method);
    }
}
