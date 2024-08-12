namespace BookingService.Bookings.Api.Contracts.Booking.Dtos;

public class BookingData
{
	public long BookingId {  get; set; }
	public BookingStatus OrderStatus { get; set; }
	public long UserId { get; set; }
	public long ResourceId { get; set; }
	public DateOnly BookedFrom { get; set; }
	public DateOnly BookedTo { get; set; }
	public DateTimeOffset? BookingOrderTimestamp { get; set; }
}