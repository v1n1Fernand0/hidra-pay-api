using HidraPay.Domain.Enums;

namespace HidraPay.Application.UseCases.GetStatus
{
    public interface IGetStatusUseCase
    {
        Task<PaymentStatus> ExecuteAsync(string transactionId, PaymentMethod method);
    }
}
