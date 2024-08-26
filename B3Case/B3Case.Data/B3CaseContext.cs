using B3Case.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace B3Case.Data
{
    public class B3CaseContext : DbContext
    {
        public B3CaseContext(DbContextOptions<B3CaseContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var assembly = typeof(B3CaseContext).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.Date).HasColumnName("date");
            });

            modelBuilder
                .Entity<Order>()
                .Property(p => p.Date)
                .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        }

        public DbSet<Order> Order { get; set; }
    }
}
