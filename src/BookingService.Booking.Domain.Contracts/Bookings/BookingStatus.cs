namespace BookingService.Booking.Domain.Contracts.Bookings;

public enum BookingStatus
{
	None = 0,
	AwaitConfirmation = 1,
	Confirmed = 2,
	Cancelled = 3
}