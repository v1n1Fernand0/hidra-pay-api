using HidraPay.Application.Interfaces;
using HidraPay.Domain.Enums;
using HidraPay.Domain.Ports;
using HidraPay.Domain.ValueObjects;
using System.Net.Http.Json;

namespace HidraPay.Infrastructure.Gateways.AbacatePay
{
    public class AbacatePayGateway : IPaymentGateway, IFeaturesMethod
    {
        // Agora este gateway suporta múltiplos métodos
        public IReadOnlyCollection<PaymentMethod> SupportedMethods
            => new[]
            {
                PaymentMethod.CreditCard,
                PaymentMethod.Pix,
                PaymentMethod.BankSlip
            };

        private readonly HttpClient _http;

        public AbacatePayGateway(HttpClient http)
        {
            _http = http;
        }

        public async Task<PaymentResult> AuthorizeAsync(PaymentRequest request)
        {
            var body = new
            {
                amount = request.Amount,
                currency = request.Currency,
                orderId = request.OrderId,
                expiresAt = request.ExpiresAt?.ToString("o")
            };

            var resp = await _http.PostAsJsonAsync("/v1/charges", body);
            resp.EnsureSuccessStatusCode();

            var dto = await resp.Content.ReadFromJsonAsync<AbacatePayChargeDto>()
                      ?? throw new InvalidOperationException("Resposta inválida do AbacatePay");

            var status = dto.Status switch
            {
                "paid" => PaymentStatus.Captured,
                "pending" => PaymentStatus.Pending,
                "failed" => PaymentStatus.Failed,
                _ => PaymentStatus.Unknown
            };

            return new PaymentResult(dto.Id, status, dto.Amount);
        }

        public Task<PaymentResult> CaptureAsync(string transactionId, decimal amount)
            => Task.FromException<PaymentResult>(
                   new NotSupportedException("Capture não aplicável para Pix/Boleto"));

        public async Task<PaymentResult> RefundAsync(string transactionId, decimal amount)
        {
            var resp = await _http.PostAsync($"/v1/refunds/{transactionId}", null);
            resp.EnsureSuccessStatusCode();

            var dto = await resp.Content.ReadFromJsonAsync<AbacatePayRefundDto>()
                      ?? throw new InvalidOperationException("Resposta inválida do AbacatePay");

            var status = dto.Status == "refunded"
                ? PaymentStatus.Refunded
                : PaymentStatus.Unknown;

            return new PaymentResult(dto.Id, status, dto.Amount);
        }

        public async Task<PaymentStatus> GetStatusAsync(string transactionId)
        {
            var resp = await _http.GetAsync($"/v1/charges/{transactionId}");
            resp.EnsureSuccessStatusCode();

            var dto = await resp.Content.ReadFromJsonAsync<AbacatePayChargeDto>()
                      ?? throw new InvalidOperationException("Resposta inválida do AbacatePay");

            return dto.Status switch
            {
                "paid" => PaymentStatus.Captured,
                "pending" => PaymentStatus.Pending,
                "failed" => PaymentStatus.Failed,
                _ => PaymentStatus.Unknown
            };
        }
    }
}
