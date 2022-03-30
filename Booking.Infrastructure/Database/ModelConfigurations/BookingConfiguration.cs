using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.Database.ModelConfigurations;

public class BookingConfiguration : IEntityTypeConfiguration<Domain.Entities.Booking>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Booking> entity)
    {
        entity.ToTable("Booking");
        entity.HasKey(b => b.Id);
        entity.Property(b => b.Id).HasColumnName("BookingId");

        entity.Property(a => a.Start)
            .HasColumnName("Begin")
            .IsRequired();

        entity.Property(a => a.Slut)
            .HasColumnName("End")
            .IsRequired();

        entity.Ignore(b => b.ServiceProvider);

        entity.Property(a => a.Version).IsRowVersion();
    }
}