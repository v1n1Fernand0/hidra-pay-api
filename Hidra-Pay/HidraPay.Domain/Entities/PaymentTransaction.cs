using HidraPay.Domain.Enums;

namespace HidraPay.Domain.Entities
{
    /// <summary>
    /// Representa uma transação de pagamento persistida.
    /// </summary>
    public class PaymentTransaction
    {
        public Guid Id { get; private set; }
        public string TransactionId { get; private set; } = string.Empty;
        public string OrderId { get; private set; } = string.Empty; 
        public decimal Amount { get; private set; }
        public string Currency { get; private set; } = string.Empty; 
        public PaymentMethod Method { get; private set; }
        public PaymentStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private PaymentTransaction() { }

        public PaymentTransaction(
            string orderId,
            string transactionId,
            decimal amount,
            string currency,
            PaymentMethod method,
            PaymentStatus status)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            TransactionId = transactionId;
            Amount = amount;
            Currency = currency;
            Method = method;
            Status = status;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateStatus(PaymentStatus newStatus)
        {
            Status = newStatus;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
