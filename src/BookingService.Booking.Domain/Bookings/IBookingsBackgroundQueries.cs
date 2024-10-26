namespace BookingService.Booking.Domain.Bookings;

public interface IBookingsBackgroundQueries
{
	Task<IReadOnlyCollection<BookingAggregate>> GetConfirmationAwaitingBookings(int count = 10);
}