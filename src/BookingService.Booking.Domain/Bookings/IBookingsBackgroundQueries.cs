namespace BookingService.Booking.Domain.Bookings;

public interface IBookingsBackgroundQueries
{
	Task<BookingAggregate?> GetBookingByRequestId(Guid requestId, CancellationToken cancellationToken = default);
}