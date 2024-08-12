namespace BookingService.Bookings.Api.Contracts.Booking.Requests;

public record CreateBookingRequest()
{
	public long BookingId { get; set; }
	public long ResourceId { get; set; }
	public DateOnly StartDate { get; set; }
	public DateOnly EndDate { get; set; }
}