namespace BookingService.Bookings.Api.Contracts.Booking.Requests;

public record GetBookingsByFilterRequest()
{
	public long BookingId { get; set; }
}