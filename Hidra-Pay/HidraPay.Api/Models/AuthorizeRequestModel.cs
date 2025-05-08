using HidraPay.Domain.Enums;

namespace HidraPay.Api.Models
{
    public class AuthorizeRequestModel
    {
        public string OrderId { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = default!;
        public PaymentMethod Method { get; set; }   // mudou para enum
        public DateTime? ExpiresAt { get; set; }
    }
}
