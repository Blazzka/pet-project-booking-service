using BookingService.Booking.Domain.Booking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingService.Booking.Persistence.Configurations;

public class BookingAggregateConfiguration : IEntityTypeConfiguration<BookingAggregate>
{
	public void Configure(EntityTypeBuilder<BookingAggregate> builder)
	{
		builder.ToTable("Bookings");

		builder.HasKey(x => x.Id)
			.HasName("pk_bookings");

		builder.Property(x => x.Status)
			.HasColumnName("status");
			
		builder.Property(x => x.UserId)
			.HasColumnName("user_id");
			
		builder.Property(x => x.ResourceId)
			.HasColumnName("resource_id");
			
		builder.Property(x => x.BookedFrom)
			.HasColumnName("booked_from");
		
		builder.Property(x => x.BookedTo)
			.HasColumnName("booked_to");
		
		builder.Property(x => x.CreatedAt)
			.HasColumnName("created_at");
	}
}