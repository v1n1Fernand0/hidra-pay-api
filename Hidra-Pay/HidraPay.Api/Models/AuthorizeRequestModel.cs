namespace HidraPay.Api.Models
{
    public class AuthorizeRequestModel
    {
        public string OrderId { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = default!;
        public string Method { get; set; } = default!;  
        public DateTime? ExpiresAt { get; set; }       
    }
}
