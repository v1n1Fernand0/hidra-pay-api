using HidraPay.Domain.Enums;

namespace HidraPay.Api.Models
{
    public class PaymentResponseModel
    {
        public string TransactionId { get; set; } = default!;
        public PaymentStatus Status { get; set; }
        public decimal Amount { get; set; }
    }
}
