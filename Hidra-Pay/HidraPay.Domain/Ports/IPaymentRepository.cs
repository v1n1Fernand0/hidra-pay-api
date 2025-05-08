using HidraPay.Domain.Entities;

namespace HidraPay.Domain.Ports
{
    public interface IPaymentRepository
    {
        Task AddAsync(PaymentTransaction transaction);
        Task<PaymentTransaction?> GetByTransactionIdAsync(string transactionId);
        Task UpdateAsync(PaymentTransaction transaction);
    }
}
