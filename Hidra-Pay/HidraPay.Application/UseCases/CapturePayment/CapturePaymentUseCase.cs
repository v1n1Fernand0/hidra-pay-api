using HidraPay.Application.Constants;
using HidraPay.Application.Services;
using HidraPay.Application.UseCases.CapturePayment;
using HidraPay.Domain.Enums;
using HidraPay.Domain.Ports;
using HidraPay.Domain.ValueObjects;

public class CapturePaymentUseCase : ICapturePaymentUseCase
{
    private readonly PaymentGatewayFactory _factory;
    private readonly IPaymentRepository _repo;

    public CapturePaymentUseCase(
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
                string.Format(ErrorMessages.MethodNotSupported, txId));


        var result = await _factory.GetGateway(method)
            .CaptureAsync(txId, amount);

        transaction.UpdateStatus(result.Status);
        await _repo.UpdateAsync(transaction);

        return result;
    }
}
