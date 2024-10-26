using BookingService.Booking.Domain.Bookings;
using BookingService.Booking.Domain.Contracts.Bookings;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Booking.Persistence;

public class BookingsBackgroundQueries : IBookingsBackgroundQueries
{
	private readonly BookingsContext _context;

	public BookingsBackgroundQueries(BookingsContext context)
	{
		_context = context;
	}

	public async Task<IReadOnlyCollection<BookingAggregate>> GetConfirmationAwaitingBookings(int count = 10)
	{
		var bookings = await _context.Bookings
			.Where(b => b.Status == BookingStatus.AwaitConfirmation)
			.OrderBy(b => b.Id)
			.Take(count)
			.ToListAsync();

		return bookings.AsReadOnly();
	}
}