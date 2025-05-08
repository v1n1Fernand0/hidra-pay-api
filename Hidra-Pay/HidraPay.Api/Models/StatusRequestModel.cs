using HidraPay.Domain.Enums;

namespace HidraPay.Api.Models
{
    /// <summary>
    /// Modelo para consulta de status de pagamento.
    /// </summary>
    public class StatusRequestModel
    {
        public string TransactionId { get; set; } = default!;
        public PaymentMethod Method { get; set; }
    }
}
