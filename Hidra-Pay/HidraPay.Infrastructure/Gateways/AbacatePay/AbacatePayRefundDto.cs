namespace HidraPay.Infrastructure.Gateways.AbacatePay
{
    internal class AbacatePayRefundDto
    {
        public string Id { get; set; } = default!;
        public string Status { get; set; } = default!;
        public decimal Amount { get; set; }
    }
}
