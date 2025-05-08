using HidraPay.Application.Interfaces;
using HidraPay.Domain.Enums;
using HidraPay.Domain.Ports;
using HidraPay.Domain.ValueObjects;
using Stripe;
using DomainPaymentMethod = HidraPay.Domain.Enums.PaymentMethod;

namespace HidraPay.Infrastructure.Gateways
{
    public class StripeGateway : IPaymentGateway, IFeaturesMethod
    {
        public IReadOnlyCollection<DomainPaymentMethod> SupportedMethods
            => new[] { DomainPaymentMethod.CreditCard, DomainPaymentMethod.BankSlip };

        private readonly PaymentIntentService _intentService;

        public StripeGateway(PaymentIntentService intentService)
        {
            _intentService = intentService
                ?? throw new ArgumentNullException(nameof(intentService));
        }

        public async Task<PaymentResult> AuthorizeAsync(PaymentRequest request)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(request.Amount * 100),
                Currency = request.Currency.ToLower(),
                CaptureMethod = "manual",
                Metadata = new Dictionary<string, string>
                {
                    ["orderId"] = request.OrderId
                }
            };
            var intent = await _intentService.CreateAsync(options);
            var status = intent.Status switch
            {
                "requires_capture" => PaymentStatus.Authorized,
                "succeeded" => PaymentStatus.Captured,
                _ => PaymentStatus.Unknown
            };

            // AmountReceived já é long, sem nullable
            var received = intent.AmountReceived;
            return new PaymentResult(
                intent.Id,
                status,
                received / 100m
            );
        }

        public async Task<PaymentResult> CaptureAsync(string transactionId, decimal amount)
        {
            var options = new PaymentIntentCaptureOptions
            {
                AmountToCapture = (long)(amount * 100)
            };
            var intent = await _intentService.CaptureAsync(transactionId, options);
            var status = intent.Status == "succeeded"
                ? PaymentStatus.Captured
                : PaymentStatus.Unknown;

            var received = intent.AmountReceived;
            return new PaymentResult(
                intent.Id,
                status,
                received / 100m
            );
        }

        public async Task<PaymentResult> RefundAsync(string transactionId, decimal amount)
        {
            var refundService = new RefundService();
            var options = new RefundCreateOptions
            {
                PaymentIntent = transactionId,
                Amount = (long)(amount * 100)
            };
            var refund = await refundService.CreateAsync(options);
            var status = refund.Status == "succeeded"
                ? PaymentStatus.Refunded
                : PaymentStatus.Unknown;

            // Refund.Amount já é long
            var refundedAmount = refund.Amount;
            return new PaymentResult(
                refund.Id,
                status,
                refundedAmount / 100m
            );
        }

        public async Task<PaymentStatus> GetStatusAsync(string transactionId)
        {
            var intent = await _intentService.GetAsync(transactionId);
            return intent.Status switch
            {
                "requires_capture" => PaymentStatus.Authorized,
                "succeeded" => PaymentStatus.Captured,
                "canceled" => PaymentStatus.Failed,
                _ => PaymentStatus.Unknown
            };
        }
    }
}
