namespace HidraPay.Infrastructure.Gateways.AbacatePay
{
    internal class AbacatePayChargeDto
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
    }
}
