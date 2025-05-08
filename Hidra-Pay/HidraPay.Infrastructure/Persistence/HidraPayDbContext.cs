using HidraPay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HidraPay.Infrastructure.Persistence
{
    public class HidraPayDbContext : DbContext
    {
        public HidraPayDbContext(DbContextOptions<HidraPayDbContext> opts)
        : base(opts)
        {
        }

        public DbSet<PaymentTransaction> PaymentTransactions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var pt = modelBuilder.Entity<PaymentTransaction>();
            pt.HasKey(x => x.Id);
            pt.Property(x => x.OrderId).IsRequired();
            pt.Property(x => x.TransactionId).IsRequired();
            pt.Property(x => x.Amount).HasColumnType("decimal(18,2)");
            pt.Property(x => x.Currency).HasMaxLength(3).IsRequired();
            pt.Property(x => x.Method).IsRequired();
            pt.Property(x => x.Status).IsRequired();
            pt.Property(x => x.CreatedAt).IsRequired();
            pt.Property(x => x.UpdatedAt);
            pt.HasIndex(x => x.TransactionId).IsUnique();
        }
    }
}
