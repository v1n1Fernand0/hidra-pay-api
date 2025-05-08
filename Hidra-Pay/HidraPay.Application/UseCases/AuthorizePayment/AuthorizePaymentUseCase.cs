using HidraPay.Application.Services;
using HidraPay.Application.UseCases.AuthorizePayment;
using HidraPay.Domain.Entities;
using HidraPay.Domain.Ports;
using HidraPay.Domain.ValueObjects;

public class AuthorizePaymentUseCase : IAuthorizePaymentUseCase
{
    private readonly PaymentGatewayFactory _factory;
    private readonly IPaymentRepository _repo;

    public AuthorizePaymentUseCase(
        PaymentGatewayFactory factory,
        IPaymentRepository repo)
    {
        _factory = factory;
        _repo = repo;
    }

    public async Task<PaymentResult> ExecuteAsync(PaymentRequest request)
    {
        var result = await _factory
            .GetGateway(request.Method)
            .AuthorizeAsync(request);

        var transaction = new PaymentTransaction(
            orderId: request.OrderId,
            transactionId: result.TransactionId,
            amount: result.Amount,
            currency: request.Currency,
            method: request.Method,
            status: result.Status
        );
        await _repo.AddAsync(transaction);

        return result;
    }
}
