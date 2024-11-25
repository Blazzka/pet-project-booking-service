using BookingService.Booking.Domain.Bookings;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Booking.Persistence;

public class BookingsBackgroundQueries : IBookingsBackgroundQueries
{
	private readonly BookingsContext _context;

	public BookingsBackgroundQueries(BookingsContext context)
	{
		_context = context;
	}

	public async Task<BookingAggregate?> GetBookingByRequestId(Guid requestId,
		CancellationToken cancellationToken = default)
	{
		return await _context.Bookings.FirstOrDefaultAsync(b => b.CatalogRequestId == requestId, cancellationToken);
	}
}