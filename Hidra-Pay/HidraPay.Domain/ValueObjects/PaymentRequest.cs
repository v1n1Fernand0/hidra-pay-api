using HidraPay.Domain.Enums;

namespace HidraPay.Domain.ValueObjects
{
    public record PaymentRequest(
       string OrderId,
       decimal Amount,
       string Currency,
       PaymentMethod Method,
       DateTime? ExpiresAt = null
   );
}
