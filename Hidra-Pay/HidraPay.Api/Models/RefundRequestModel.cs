using HidraPay.Domain.Enums;

namespace HidraPay.Api.Models
{
    /// <summary>
    /// Modelo para requisição de estorno de pagamento.
    /// </summary>
    public class RefundRequestModel
    {
        public string TransactionId { get; set; } = default!;
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
    }
}
