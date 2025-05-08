namespace HidraPay.Domain.Events
{
    public class PaymentCapturedEvent
    {
        public string TransactionId { get; }
        public decimal Amount { get; }

        public PaymentCapturedEvent(string transactionId, decimal amount)
        {
            TransactionId = transactionId;
            Amount = amount;
        }
    }
}
