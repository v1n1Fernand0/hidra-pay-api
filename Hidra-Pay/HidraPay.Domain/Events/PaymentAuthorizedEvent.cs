// PaymentAuthorizedEvent.cs
namespace HidraPay.Domain.Events
{
    public class PaymentAuthorizedEvent
    {
        public string TransactionId { get; }
        public string OrderId { get; }
        public decimal Amount { get; }

        public PaymentAuthorizedEvent(string transactionId, string orderId, decimal amount)
        {
            TransactionId = transactionId;
            OrderId = orderId;
            Amount = amount;
        }
    }
}
