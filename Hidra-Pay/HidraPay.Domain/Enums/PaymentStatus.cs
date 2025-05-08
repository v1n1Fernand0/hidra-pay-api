namespace HidraPay.Domain.Enums
{
    public enum PaymentStatus
    {
        Pending,     
        Authorized,  
        Captured,    
        Refunded,    
        Failed,
        Expired,
        Unknown, 
    }
}
