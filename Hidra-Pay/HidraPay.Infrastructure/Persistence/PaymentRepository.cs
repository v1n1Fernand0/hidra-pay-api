using HidraPay.Domain.Entities;
using HidraPay.Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace HidraPay.Infrastructure.Persistence
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly HidraPayDbContext _ctx;

        public PaymentRepository(HidraPayDbContext ctx)
            => _ctx = ctx;

        public async Task AddAsync(PaymentTransaction transaction)
        {
            _ctx.PaymentTransactions.Add(transaction);
            await _ctx.SaveChangesAsync();
        }

        public Task<PaymentTransaction?> GetByTransactionIdAsync(string transactionId)
        {
            return _ctx.PaymentTransactions
                       .FirstOrDefaultAsync(x => x.TransactionId == transactionId);
        }

        public async Task UpdateAsync(PaymentTransaction transaction)
        {
            _ctx.PaymentTransactions.Update(transaction);
            await _ctx.SaveChangesAsync();
        }
    }
}
