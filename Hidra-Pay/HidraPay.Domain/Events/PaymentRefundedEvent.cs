namespace HidraPay.Domain.Events
{
    public class PaymentRefundedEvent
    {
        public string TransactionId { get; }
        public decimal Amount { get; }

        public PaymentRefundedEvent(string transactionId, decimal amount)
        {
            TransactionId = transactionId;
            Amount = amount;
        }
    }
}
