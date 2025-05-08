using HidraPay.Domain.ValueObjects;

namespace HidraPay.Application.UseCases.CapturePayment
{
    /// <summary>
    /// Caso de uso: capturar um pagamento previamente autorizado.
    /// </summary>
    public interface ICapturePaymentUseCase
    {
        Task<PaymentResult> ExecuteAsync(string transactionId, decimal amount, HidraPay.Domain.Enums.PaymentMethod method);
    }
}
