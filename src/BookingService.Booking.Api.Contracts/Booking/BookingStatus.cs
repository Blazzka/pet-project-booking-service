namespace BookingService.Booking.Api.Contracts.Booking;

public enum BookingStatus
{
	None = 0,
	AwaitConfirmation = 1,
	Confirmed = 2,
	Cancelled = 3
}