using BookingService.Booking.Domain.Booking;
using BookingService.Booking.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Booking.Persistence;

public class BookingsContext : DbContext
{
	public BookingsContext(DbContextOptions<BookingsContext> options) : base(options)
	{
		// Инициализация
	}
	public DbSet<BookingAggregate> Bookings { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new BookingAggregateConfiguration());
		base.OnModelCreating(modelBuilder);
	}
}