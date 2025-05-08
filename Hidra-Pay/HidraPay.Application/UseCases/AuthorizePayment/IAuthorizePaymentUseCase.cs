using HidraPay.Domain.ValueObjects;

namespace HidraPay.Application.UseCases.AuthorizePayment
{
    /// <summary>
    /// Caso de uso: autorizar um pagamento.
    /// </summary>
    public interface IAuthorizePaymentUseCase
    {
        Task<PaymentResult> ExecuteAsync(PaymentRequest request);
    }
}
