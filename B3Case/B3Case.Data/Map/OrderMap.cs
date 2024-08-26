using B3Case.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B3Case.Data.Map
{
    public class OrderMap
    {
        public OrderMap(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasKey(x => x.Id)
                .HasName("id");

            builder
                .Property(b => b.Id)
                .HasColumnType("integer")
                .HasColumnName("id")
                .IsRequired();

            builder
                .Property(b => b.Description)
                .HasColumnType("varchar")
                .HasColumnName("description")
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(b => b.Status)
                .HasColumnType("varchar")
                .HasColumnName("status")
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(b => b.Date)
                .HasColumnType("timestamptz")
                .HasColumnName("date")
                .IsRequired();
        }
    }
}
