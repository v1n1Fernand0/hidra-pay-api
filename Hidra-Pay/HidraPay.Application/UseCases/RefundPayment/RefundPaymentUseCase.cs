using System;
using System.Threading.Tasks;
using HidraPay.Application.Constants;
using HidraPay.Application.Services;
using HidraPay.Application.UseCases.RefundPayment;
using HidraPay.Domain.Enums;
using HidraPay.Domain.Ports;
using HidraPay.Domain.ValueObjects;

namespace HidraPay.Application.UseCases.RefundPayment
{
    public class RefundPaymentUseCase : IRefundPaymentUseCase
    {
        private readonly PaymentGatewayFactory _factory;
        private readonly IPaymentRepository _repo;

        public RefundPaymentUseCase(
            PaymentGatewayFactory factory,
            IPaymentRepository repo)
        {
            _factory = factory;
            _repo = repo;
        }

        public async Task<PaymentResult> ExecuteAsync(string txId, decimal amount, PaymentMethod method)
        {
            var transaction = await _repo.GetByTransactionIdAsync(txId)
                ?? throw new InvalidOperationException(
                    string.Format(ErrorMessages.TransactionNotFound, txId));

            var result = await _factory
                .GetGateway(method)
                .RefundAsync(txId, amount);

            transaction.UpdateStatus(result.Status);
            await _repo.UpdateAsync(transaction);

            return result;
        }
    }
}
