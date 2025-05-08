using HidraPay.Domain.Enums;
using HidraPay.Domain.ValueObjects;

namespace HidraPay.Domain.Ports
{
    /// <summary>
    /// Abstração para operações de pagamento.
    /// </summary>
    public interface IPaymentGateway
    {
        /// <summary>
        /// Autoriza (ou cria) o pagamento conforme o método.
        /// </summary>
        Task<PaymentResult> AuthorizeAsync(PaymentRequest request);

        /// <summary>
        /// Captura um pagamento já autorizado.
        /// </summary>
        Task<PaymentResult> CaptureAsync(string transactionId, decimal amount);

        /// <summary>
        /// Reembolsa um pagamento já capturado.
        /// </summary>
        Task<PaymentResult> RefundAsync(string transactionId, decimal amount);

        /// <summary>
        /// Consulta o status atual de uma transação.
        /// </summary>
        Task<PaymentStatus> GetStatusAsync(string transactionId);
    }
}
