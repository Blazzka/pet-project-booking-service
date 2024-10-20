namespace BookingService.Booking.Domain.Bookings;

public interface IBookingsBackgroundQueries
{
	IReadOnlyCollection<BookingAggregate> GetConfirmationAwaitingBookings(int count = 10);
}