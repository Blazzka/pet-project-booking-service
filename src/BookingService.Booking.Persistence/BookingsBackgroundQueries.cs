using BookingService.Booking.Domain.Bookings;
using BookingService.Booking.Domain.Contracts.Bookings;

namespace BookingService.Booking.Persistence;

public class BookingsBackgroundQueries : IBookingsBackgroundQueries
{
	private readonly BookingsContext _context;

	public BookingsBackgroundQueries(BookingsContext context)
	{
		_context = context;
	}

	public IReadOnlyCollection<BookingAggregate> GetConfirmationAwaitingBookings(int count = 10)
	{
		var bookings = _context.Bookings
			.Where(b => b.Status == (BookingStatus)1)
			.OrderByDescending(b => b.Id)
			.Take(count)
			.ToList();

		return bookings.AsReadOnly();
	}
}